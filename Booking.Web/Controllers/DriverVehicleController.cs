using Booking.Application.Interfaces;
using Booking.Application.Services;
using Booking.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class DriverVehicleController(ILogger<DriverVehicleController> logger,
        IDriverVehicleService driverVehicleService) : BaseController
    {
        private readonly ILogger<DriverVehicleController> _logger = logger;
        private readonly IDriverVehicleService _driverVehicleService = driverVehicleService;
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
    }
}
