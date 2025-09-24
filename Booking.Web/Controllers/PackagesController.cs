using Booking.Application.DTOs.Tour;
using Booking.Application.Interfaces;
using Booking.Web.Models;
using ImageMagick;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.Versioning;

namespace Booking.Web.Controllers
{
    public class PackagesController(ILogger<PackagesController> logger, IPackageService packageService, IPackageCategoryService packageCategoryService) : BaseController
    {
        private readonly ILogger<PackagesController> _logger = logger;
        private readonly IPackageService _packageService = packageService;
        private readonly IPackageCategoryService _packageCategoryService = packageCategoryService;


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
        private async Task<TourPackageMediaDto> ProcessAndSaveFile(IFormFile file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            var thumbsFolder = Path.Combine(uploadsFolder, "thumbs");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);
            if (!Directory.Exists(thumbsFolder))
                Directory.CreateDirectory(thumbsFolder);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            var thumbPath = Path.Combine(thumbsFolder, uniqueFileName);

            // Save original file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            const int size = 150;
            const int quality = 75;
            // Create thumbnail (requires System.Drawing.Common NuGet package)
            using (var image = new MagickImage(filePath))
            {
                image.Resize(size, size);
                image.Strip();
                image.Quality = quality;
                image.Write(thumbsFolder);
            }
            return new TourPackageMediaDto
            {
                FileName = uniqueFileName,
                OriginalFileName = fileName,
                FilePath = "/uploads/" + uniqueFileName,
                ThumbnailPath = "/uploads/thumbs/" + uniqueFileName,
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
            return await Task.Run(() => { return View(); });
        }
        [HttpPost]
        public async Task<IActionResult> Single(IFormFile file)
        {
            try
            {
                return Json(new { success = true, files = await ProcessAndSaveFile(file) });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file");
                return Json(new { success = false, message = "An error occurred while uploading files." });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Multiple(List<IFormFile> files)
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

                    var fileResult = await ProcessAndSaveFile(file);
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
