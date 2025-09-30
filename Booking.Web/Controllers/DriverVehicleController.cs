using Booking.Application.DTOs;
using Booking.Application.DTOs.Tour;
using Booking.Application.Interfaces;
using Booking.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Booking.Web.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class DriverVehicleController(ILogger<DriverVehicleController> logger, IDriverService driverService,
        IDriverVehicleService driverVehicleService, IVehicleService vehicleService, IOptions<GoogleSettings> options,
        IWebHostEnvironment webHostEnvironment) : BaseController
    {
        private readonly ILogger<DriverVehicleController> _logger = logger;
        private readonly IDriverVehicleService _driverVehicleService = driverVehicleService;
        private readonly IDriverService _driverService = driverService;
        private readonly IVehicleService _vehicleService = vehicleService;
        private readonly GoogleSettings _settings = options.Value;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
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
                FilePath = "/uploads/" + uniqueFileName, //"TODO: Change to CDN Path",
                ThumbnailPath = "/uploads/" + uniquethumbFileName, //"TODO: Change to CDN Path",
                FileSize = file.Length,
                FileType = Path.GetExtension(filePath)
            };
        }
        public async Task<IActionResult> Index(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View();
            }, token);
        }
        [ValidateAntiForgeryToken, HttpPost]
        public async Task<IActionResult> LoadDriverVehicleData([FromBody] DataTableAjaxPostModel request, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid request data." });
            }

            try
            {
                string search = "";
                if (!String.IsNullOrEmpty(request.search?.value))
                    search = request.search?.value ?? string.Empty;
                var result = await _driverVehicleService.DriverVehicleList(search, request.length, request.start, token);
                return Json(new
                {
                    draw = request.draw == 0 ? 1 : request.draw,
                    recordsFiltered = result.Filtered,
                    recordsTotal = result.Total,
                    data = result.DriverVehicle.AsParallel().ToArray()
                });
            }
            catch (Exception ex)
            {
                return Json("Something went wrong {0}", ex);
            }
        }
        [ValidateAntiForgeryToken, HttpPost]
        public async Task<IActionResult> RejectDriver(int DriverId, int VehicleId, CancellationToken token)
        {
            await _driverVehicleService.RejectDriverVehicleAsync(DriverId, VehicleId, token);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddSchedule(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View("Index");
            }, token);
        }

        [ValidateAntiForgeryToken, HttpPost]
        public async Task<IActionResult> AddSchedule(int DriverId, int VehicleId, CancellationToken token)
        {
            var driver = await _driverService.GetDriverAsync(DriverId, token);
            var vehicle = await _vehicleService.GetVehicleAsync(VehicleId, token);
            ViewBag.ApiKey = _settings.PlacesApiKey;
            QuickAssignmentViewModel model = new()
            {
                Vehicle = vehicle,
                Driver = driver,
            };
            return View("QuickAssign", model);
        }
        [ValidateAntiForgeryToken, HttpPost]
        public async Task<ActionResult> DriverVehicleRoutes(int DriverId, int VehicleId, CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View();
            }, token);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveDriverVehicle(CreateDriverVehicleDto model, CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return RedirectToAction("Index");
            }, token);
        }

        public async Task<IActionResult> Create(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View();
            }, token);
        }
        [HttpPost]
        public async Task<IActionResult> SaveDriverPhoto(IFormFile file, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid file upload request." });
            }
            try
            {
                var uploadedFiles = new List<TourPackageMediaDto>
                {
                    await ProcessAndSaveFile(file, token)
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
        public async Task<IActionResult> SaveVehicleImage(IFormFile file, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid file upload request." });
            }
            try
            {
                var uploadedFiles = new List<TourPackageMediaDto>
                {
                    await ProcessAndSaveFile(file, token)
                };
                return Json(new { success = true, files = uploadedFiles });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file");
                return Json(new { success = false, message = "An error occurred while uploading files." });
            }
        }
    }
}
