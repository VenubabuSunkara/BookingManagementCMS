using CsvHelper;
using OfficeOpenXml;
using System.Globalization;
using System.Text.Json;
using System.Xml.Serialization;
using System.Reflection;
using Booking.Application.Enums;

namespace Booking.Application.Services
{

    public class FileReaderService
    {
        public async Task<List<T>> ReadAsync<T>(Stream stream, FileType fileType) where T : class, new()
        {
            return fileType switch
            {
                FileType.Excel => await ReadExcelAsync<T>(stream),
                FileType.Csv => await ReadCsvAsync<T>(stream),
                FileType.Json => await ReadJsonAsync<T>(stream),
                FileType.Xml => await ReadXmlAsync<T>(stream),
                _ => throw new NotSupportedException($"File type {fileType} not supported")
            };
        }

        private async Task<List<T>> ReadExcelAsync<T>(Stream stream) where T : new()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets[0];
            var rowCount = worksheet.Dimension.Rows;
            var colCount = worksheet.Dimension.Columns;

            var list = new List<T>();

            // Read headers
            var headers = new Dictionary<int, string>();
            for (int col = 1; col <= colCount; col++)
            {
                headers[col] = worksheet.Cells[1, col].Text.Trim();
            }

            for (int row = 2; row <= rowCount; row++)
            {
                var obj = new T();
                var type = typeof(T);

                foreach (var header in headers)
                {
                    var prop = type.GetProperty(header.Value, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (prop != null && prop.CanWrite)
                    {
                        var cellValue = worksheet.Cells[row, header.Key].Text;
                        if (!string.IsNullOrEmpty(cellValue))
                        {
                            object convertedValue = Convert.ChangeType(cellValue, prop.PropertyType);
                            prop.SetValue(obj, convertedValue);
                        }
                    }
                }

                list.Add(obj);
            }

            return await Task.FromResult(list);
        }

        private async Task<List<T>> ReadCsvAsync<T>(Stream stream) where T : class
        {
            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<T>().ToList();
            return await Task.FromResult(records);
        }

        private async Task<List<T>> ReadJsonAsync<T>(Stream stream) where T : class
        {
            using var reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync();

            var result = JsonSerializer.Deserialize<List<T>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result ?? [];
        }

        private async Task<List<T>> ReadXmlAsync<T>(Stream stream) where T : class
        {
            var serializer = new XmlSerializer(typeof(List<T>));
            var result = (List<T>?)serializer.Deserialize(stream);
            return await Task.FromResult(result ?? new List<T>());
        }
    }

}
