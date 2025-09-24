using Booking.Application.DTOs.Tour;
using Booking.Application.Interfaces;
using Booking.Web.Models;
using DocumentFormat.OpenXml.Drawing.Charts;
using ImageMagick;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org.BouncyCastle.Utilities.Zlib;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using StackExchange.Profiling.Internal;
using System.Runtime.Versioning;
using System.Text.Json;
using Size = SixLabors.ImageSharp.Size;

namespace Booking.Web.Controllers
{
    public class PackagesController(ILogger<PackagesController> logger, IPackageService packageService,
        IPackageCategoryService packageCategoryService, IWebHostEnvironment webHostEnvironment) : BaseController
    {
        private readonly ILogger<PackagesController> _logger = logger;
        private readonly IPackageService _packageService = packageService;
        private readonly IPackageCategoryService _packageCategoryService = packageCategoryService;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        #region private member functions
        private (bool IsValid, string Message) ValidateFile(IFormFile file, int _maxFileSize, string[] _allowedExtensions)
        {
            // Check file size
            if (file.Length > _maxFileSize)
                return (false, "File size exceeds maximum limit");

            // Check file extension
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(extension))
                return (false, "File type not allowed");

            return (true, string.Empty);
        }
        private async Task<TourPackageMediaDto> ProcessAndSaveFile(IFormFile file, CancellationToken token)
        {
            var fileName = Path.GetFileName(file.FileName);
            var uniqueId = Guid.NewGuid().ToString();
            var uniqueFileName = $"{uniqueId}_{fileName}";
            var uniquethumbFileName = $"{uniqueId}_thumb_{fileName}";
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            var thumbPath = Path.Combine(uploadsFolder, uniquethumbFileName);

            // Save original file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream, token);
            }
            using (Image image = Image.Load(filePath))
            {
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(200, 200),
                    Mode = ResizeMode.Max,
                }));
                await image.SaveAsync(thumbPath, token);
            }
            return new TourPackageMediaDto
            {
                FileName = uniqueFileName,
                OriginalFileName = fileName,
                FilePath = "/uploads/" + uniqueFileName,
                ThumbnailPath = "/uploads/" + uniquethumbFileName,
                FileSize = file.Length,
                FileType = Path.GetExtension(filePath)
            };
        }
        #endregion

        public async Task<IActionResult> Index(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View("Index");
            }, token);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetAllPackages([FromBody] DataTableAjaxPostModel request,
        CancellationToken cancellationToken)
        {
            var result = await _packageService.GetPackages(request.start, request.length, "", 0);
            return Json(new
            {
                draw = request.draw == 0 ? 1 : request.draw,
                recordsFiltered = result.FilterRecords,
                recordsTotal = result.TotalRecords,
                data = result.PackagesData.Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.DurationDays,
                    x.Destination,
                    x.ShortDescription,
                    x.Source,
                    x.BannerImage,
                    x.Price,
                }).ToArray()
            });
        }

        public async Task<IActionResult> AddCategory(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View();
            }, token);
        }

        public async Task<IActionResult> ViewPackage(int PackageId, CancellationToken token)
        {
            if (PackageId > 0)
            {

            }
            return await Task.Run(() => { return View(); });
        }
        public async Task<IActionResult> AddPackage(CancellationToken token)
        {
            var tourPackages = await _packageCategoryService.GetTourPackageCategory(token);
            PackageViewModel model = new()
            {
                PackageCategory = [.. tourPackages.Select(x => new SelectListItem()
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString()
                })]
            };

            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SavePackage(PackageViewModel model, CancellationToken token)
        {
            if (!string.IsNullOrWhiteSpace(model.SingleMediajson))
            {
                TourPackageMediaDto? banner = JsonSerializer.Deserialize<TourPackageMediaDto>(model.SingleMediajson);
            }
            if (!string.IsNullOrWhiteSpace(model.MultipleMediajson))
            {
                List<TourPackageMediaDto>? gallary = JsonSerializer.Deserialize<List<TourPackageMediaDto>>(model.MultipleMediajson);
            }
            return await Task.Run(() => { return View(); });
        }
        [HttpPost]
        public async Task<IActionResult> Single(IFormFile file, CancellationToken token)
        {
            try
            {
                var uploadedFiles = new List<TourPackageMediaDto>();
                uploadedFiles.Add(await ProcessAndSaveFile(file, token));
                return Json(new { success = true, files = uploadedFiles });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file");
                return Json(new { success = false, message = "An error occurred while uploading files." });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Multiple(List<IFormFile> files, CancellationToken token)
        {
            try
            {
                var uploadedFiles = new List<TourPackageMediaDto>();

                foreach (var file in files)
                {
                    //var validationResult = ValidateFile(file);
                    //if (!validationResult.IsValid)
                    //{
                    //    return Json(new { success = false, message = validationResult.Message });
                    //}

                    var fileResult = await ProcessAndSaveFile(file, token);
                    uploadedFiles.Add(fileResult);
                }

                return Json(new { success = true, files = uploadedFiles });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading files");
                return Json(new { success = false, message = "An error occurred while uploading files." });
            }
        }
    }
}
