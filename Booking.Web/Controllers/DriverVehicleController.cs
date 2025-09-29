using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Booking.Web.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class DriverVehicleController(ILogger<DriverVehicleController> logger, IDriverService driverService,
        IDriverVehicleService driverVehicleService, IVehicleService vehicleService, IOptions<GoogleSettings> options) : BaseController
    {
        private readonly ILogger<DriverVehicleController> _logger = logger;
        private readonly IDriverVehicleService _driverVehicleService = driverVehicleService;
        private readonly IDriverService _driverService = driverService;
        private readonly IVehicleService _vehicleService = vehicleService;
        private readonly GoogleSettings _settings = options.Value;
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

        public async Task<IActionResult> AddSchedule(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View("Index");
            }, token);

            //var driver = await _driverService.GetDriversDropdownList(token);
            //var vehicle = await _vehicleService.GetVehicleDropdownList(token);
            //ViewBag.ApiKey = _settings.PlacesApiKey;
            //QuickAssignmentViewModel model = new()
            //{
            //    Vehicles = [.. vehicle.Select(x => new SelectListItem()
            //    {
            //        Text = x.ModelName,
            //        Value = x.VehicleId.ToString()
            //    })],
            //    Drivers = [.. driver.Select(x => new SelectListItem()
            //    {
            //        Text = x.FullName,
            //        Value = x.Id.ToString()
            //    })]
            //};
            //return View("QuickAssign", model);
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

        public async Task<IActionResult> Create(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View();
            }, token);
        }
    }
}
