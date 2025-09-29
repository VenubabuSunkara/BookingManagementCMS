using Booking.Application.DTOs;
using Booking.Application.Enums;
using Booking.Application.Interfaces;
using Booking.Application.Services;
using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    public class SettingsController(ISettingService settingService, ILogger<SettingsController> logger,
        FileReaderService readerService) : BaseController
    {
        private readonly ILogger<SettingsController> _logger = logger;
        private readonly ISettingService _settingService = settingService;
        private readonly FileReaderService _readerService = readerService;
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            return await Task.Run(() => View(), cancellationToken);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoadData([FromBody] DataTableAjaxPostModel request, CancellationToken cancellationToken)
        {
            var settings = await _settingService.GetAllSettings(cancellationToken);
            return Json(new
            {
                draw = request.draw == 0 ? 1 : request.draw,
                recordsFiltered = settings.Count(),
                recordsTotal = settings.Count(),
                data = settings.Select(x => new
                {
                    x.Name,
                    x.Value,
                    x.Id,
                    x.UpdatedOn,
                }).ToArray()
            });
        }
        public async Task<IActionResult> Create(CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return View("Index");
                }, token);
            return await Task.Run(() =>
            {
                return View("Create");
            }, token);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(SettingsDto setting, CancellationToken token)
        {
            if (!ModelState.IsValid)
                return View("Create", setting);

            setting.CreatedOn = DateTime.UtcNow;
            setting.UpdatedOn = DateTime.UtcNow;
            setting.CreatedBy = base.GetUserName();
            setting.UpdatedBy = base.GetUserName();
            if (setting.Id.HasValue)
            {
                await _settingService.UpdateSetting(setting, token);
            }
            else
            {
                await _settingService.CreateSetting(setting, token);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return View("Index");
                }, token);
            var setting = await _settingService.GetSettingById(id, token);
            if (setting == null) return NotFound();
            return View("Create", setting);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return View("Index");
                }, token);
            await _settingService.DeleteSetting(Id, token);
            TempData["SuccessMessage"] = "Setting deleted successfully!";
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportCategory(IFormFile file, CancellationToken token)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            List<SettingsDto> settingsDtos;

            using var stream = file.OpenReadStream();

            if (extension == ".csv")
            {
                settingsDtos = await _readerService.ReadAsync<SettingsDto>(stream, FileType.Csv);
            }
            else if (extension == ".xlsx" || extension == ".xls")
            {
                settingsDtos = await _readerService.ReadAsync<SettingsDto>(stream, FileType.Excel);
            }
            else
            {
                return BadRequest("Unsupported file format. Only CSV or Excel allowed.");
            }

            // await _settingService.ImportSettings(null, token);

            return RedirectToAction("Index");
        }
    }
}
