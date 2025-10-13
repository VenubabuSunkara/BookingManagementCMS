using Booking.Application.DTOs.Tour;
using Booking.Application.Interfaces;
using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using StackExchange.Profiling.Internal;
using Size = SixLabors.ImageSharp.Size;

namespace Booking.Web.Controllers
{
    public class PackagesController(ILogger<PackagesController> logger, IPackageService packageService,
        IPackageCategoryService packageCategoryService, IWebHostEnvironment webHostEnvironment,
        IPackageMediaService packageMediaService, IPackageLocationService packageLocationService) : BaseController
    {
        private readonly ILogger<PackagesController> _logger = logger;
        private readonly IPackageService _packageService = packageService;
        private readonly IPackageCategoryService _packageCategoryService = packageCategoryService;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
        private readonly IPackageMediaService _packageMediaService = packageMediaService;
        private readonly IPackageLocationService _packageLocationService = packageLocationService;

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
        private async Task<TourPackageMediaDto> ProcessAndSaveFile(IFormFile file, string folderName, CancellationToken token)
        {
            var fileName = Path.GetFileName(file.FileName);
            var uniqueId = Guid.NewGuid().ToString();
            var uniqueFileName = $"{uniqueId}_{fileName}";
            var uniquethumbFileName = $"{uniqueId}_thumb_{fileName}";
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderName);
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            var thumbPath = Path.Combine(uploadsFolder, uniquethumbFileName);

            // Save original file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream, token);
            }
            using (Image image = await Image.LoadAsync(filePath, token))
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
                FilePath = $"/{folderName}/" + uniqueFileName,
                ThumbnailPath = $"/{folderName}/" + uniquethumbFileName,
                FileSize = file.Length,
                FileType = Path.GetExtension(filePath)
            };
        }
        #endregion

        public async Task<IActionResult> Index(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View();
            }, token);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetAllPackages([FromBody] DataTableAjaxPostModel request,
        CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string search = "";
            if (!String.IsNullOrEmpty(request.search?.value))
                search = request.search?.value ?? string.Empty;
            var result = await _packageService.GetPackages(request.start, request.length, search, 0, cancellationToken);
            return Json(new
            {
                draw = request.draw == 0 ? 1 : request.draw,
                recordsFiltered = result.FilterRecords,
                recordsTotal = result.TotalRecords,
                data = result.PackagesData.Select(x => new
                {
                    x.Id,
                    x.PackageName,
                    x.DurationDays,
                    Destination = x.Location.LocationName,
                    x.ShortDescription,
                    x.Source,
                    x.BannerImage,
                    x.Price,
                }).ToArray()
            });
        }

        public async Task<IActionResult> ViewPackage(int PackageId, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (PackageId == 0) return await Task.Run(() => { return View("Index"); });
            var package = await _packageService.GetPackage(PackageId, token);
            if (package == null) return View("Index");
            PackageViewModel model = new()
            {
                TourPackage = new TourPackageDto()
                {
                    Inclusions = package?.Inclusions ?? string.Empty,
                    Id = package?.Id ?? 0,
                    DurationDays = package?.DurationDays ?? string.Empty,
                    ShortDescription = package?.ShortDescription ?? string.Empty,
                    BannerImage = package?.BannerImage ?? string.Empty,
                    FullDescription = package?.FullDescription ?? string.Empty,
                    PackageName = package?.PackageName ?? string.Empty,
                    Price = package?.Price ?? 0,
                    CategoryId = package?.CategoryId ?? 0,
                    ThingsToNote = package?.ThingsToNote ?? string.Empty,
                },
                Location = package?.Location ?? new TourLocationDto(),
                PackageMedia = package?.PackageMedia ?? [],

            };
            return View(model);
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
        public async Task<IActionResult> EditPackage(int PackageId, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            var tourPackages = await _packageCategoryService.GetTourPackageCategory(token);
            var package = await _packageService.GetPackage(PackageId, token);
            if (package == null) return View("Index");
            PackageViewModel model = new()
            {
                PackageCategory = [.. tourPackages.Select(x => new SelectListItem()
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString()
                })],
                TourPackage = new TourPackageDto()
                {
                    Inclusions = package?.Inclusions ?? string.Empty,
                    Id = package?.Id ?? 0,
                    DurationDays = package?.DurationDays ?? string.Empty,
                    ShortDescription = package?.ShortDescription ?? string.Empty,
                    BannerImage = package?.BannerImage ?? string.Empty,
                    FullDescription = package?.FullDescription ?? string.Empty,
                    PackageName = package?.PackageName ?? string.Empty,
                    Price = package?.Price ?? 0,
                    CategoryId = package?.CategoryId ?? 0,
                    ThingsToNote = package?.ThingsToNote ?? string.Empty,
                },
                Location = package?.Location ?? new TourLocationDto(),
                PackageMedia = package?.PackageMedia ?? [],

            };

            return View("AddPackage", model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SavePackage(PackageViewModel model, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return View("AddPackage", model);
            }
            var bannerdata = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TourPackageMediaDto>>(model.SingleMediajson);
            if (bannerdata != null && bannerdata.Count > 0)
            {
                model.TourPackage.BannerImage = bannerdata[0].FilePath;
            }
            model.TourPackage.CategoryId = model.PackagecategoryId;
            model.TourPackage.CreatedOn = DateTime.UtcNow;
            model.TourPackage.UpdatedOn = DateTime.UtcNow;
            model.TourPackage.CreatedBy = base.GetUserName();
            model.TourPackage.UpdatedBy = base.GetUserName();
            int PackageId = await _packageService.SavePackage(model.TourPackage, token);
            if (bannerdata != null && bannerdata.Count > 0)
            {
                List<PackageMediaDto> PackageMedia = [.. bannerdata.Select(x => new PackageMediaDto()
                {
                   PackageId = PackageId,
                   MediaType= x.FileType,
                   MediaUrl=x.FilePath,
                   FileName=x.FileName,
                   IsDefault=false,
                   ThumbnailImage=x.ThumbnailPath,
                   CreatedAt=DateTime.UtcNow,
                   CreatedBy=base.GetUserName(),
                   UpdatedAt=DateTime.UtcNow,
                   UpdatedBy=base.GetUserName()
                })];
                await _packageMediaService.SavePackageMediaList(PackageMedia, token);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Single(IFormFile file, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "No files uploaded." });
            }
            try
            {
                var uploadedFiles = new List<TourPackageMediaDto>
                {
                    await ProcessAndSaveFile(file,"UploadFiles\\PackageMedia", token)
                };
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
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "No files uploaded." });
            }
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

                    var fileResult = await ProcessAndSaveFile(file, "UploadFiles\\PackageMedia", token);
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
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePackage(int PackageId, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "No files uploaded." });
            }
            if (PackageId == 0) return Json(new { success = false, message = "Invalid Package Id" });
            var result = await _packageService.DeletePackage(PackageId, token);
            if (result > 0)
                return Json(new { success = true, message = "Package deleted successfully" });
            else
                return Json(new { success = false, message = "Error deleting package" });
        }

        public async Task<IActionResult> AddGallary(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View();
            }, token);
        }
        public async Task<IActionResult> SavePackageMedia(List<IFormFile> files, int PackageId, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "No files uploaded." });
            }
            try
            {
                var uploadedFiles = new List<PackageMediaDto>();
                foreach (var file in files)
                {
                    var fileResult = await ProcessAndSaveFile(file, $"UploadFiles\\PackageMedia\\{PackageId}", token);
                    uploadedFiles.Add(new PackageMediaDto
                    {
                        PackageId = PackageId,
                        MediaType = fileResult.FileType,
                        MediaUrl = fileResult.FilePath,
                        FileName = fileResult.FileName,
                        IsDefault = false,
                        ThumbnailImage = fileResult.ThumbnailPath,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = base.GetUserName(),
                        UpdatedAt = DateTime.UtcNow,
                        UpdatedBy = base.GetUserName()
                    });
                }
                if (uploadedFiles.Count > 0)
                {
                    await _packageMediaService.SavePackageMediaList(uploadedFiles, token);
                }
                return Json(new { success = true, files = uploadedFiles });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading files");
                return Json(new { success = false, message = "An error occurred while uploading files." });
            }

        }

        public async Task<IActionResult> SavePackageLocation(List<TourLocationDto> locationDtos, int PackageId, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid data." });
            }
            return await Task.Run(() =>
            {
                return View();
            }, token);
        }
    }
}