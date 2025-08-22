using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Application.Services;
using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace Booking.Web.Controllers
{
    public class SettingsController(ISettingService settingService, ILogger<SettingsController> logger) : BaseController
    {
        private readonly ILogger<SettingsController> _logger = logger;
        private readonly ISettingService _settingService = settingService;
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            return await Task.Run(() => View(), cancellationToken);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoadData([FromBody] DataTableAjaxPostModel request, CancellationToken cancellationToken)
        {
            var roles = await _settingService.GetAllSettings();
            return Json(new
            {
                draw = request.draw == 0 ? 1 : request.draw,
                recordsFiltered = roles.Count(),
                recordsTotal = roles.Count(),
                data = roles.Select(x => new
                {
                    x.Name,
                    x.Value,
                    CreatedOn = x.CreatedOn.HasValue ? x.CreatedOn.Value.ToShortDateString() : "N/A",
                    x.Id,
                    UpdatedOn = x.UpdatedOn.HasValue ? x.UpdatedOn.Value.ToShortDateString() : "N/A",

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
                return View("Create", new SettingsDto());
            }, token);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return View("Index");
                }, token);
            var setting = await _settingService.GetSettingById(id);
            if (setting == null) return NotFound();
            return View("Create", setting);
        }
    }
}
