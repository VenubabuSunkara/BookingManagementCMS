using Booking.Application.Interfaces;
using Booking.Application.Services;
using Booking.Web.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    public class CustomerController(ICustomerService customerService) : BaseController
    {
        private readonly ICustomerService _customerService = customerService;
        public async Task<IActionResult> Index(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View();
            }, token);
        }
        public async Task<IActionResult> ExportAll(CancellationToken token)
        {
            var data = await _customerService.ExportAllAsync(token); // fetch unpaginated filtered data
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Customers");
            worksheet.Cell(1, 1).InsertTable(data);
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return File(content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        $"customersList{DateTime.Now.Date}.xlsx");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetCustomers([FromBody] DataTableAjaxPostModel request, CancellationToken token)
        {
            try
            {
                string search = "";
                if (!String.IsNullOrEmpty(request.search?.value))
                    search = request.search?.value ?? string.Empty;
                var result = await _customerService.GetAll(request.start, request.length, search, token);
                return await Task.Run(() =>
                {
                    return Json(new
                    {
                        draw = request.draw == 0 ? 1 : request.draw,
                        recordsFiltered = result.Filtered,
                        recordsTotal = result.Total,
                        data = result.CustomerDto
                    });
                }, token);
            }
            catch (Exception ex)
            {
                return Json("Something went wrong {0}", ex);
            }
        }
   
    }
}
