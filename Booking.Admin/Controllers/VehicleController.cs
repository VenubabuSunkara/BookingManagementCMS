using Booking.Application.Interfaces;
using Booking.Application.Services;
using Booking.Infrastructure.Data.Models;
using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Controllers
{
    public class VehicleController(ILogger<VehicleController> logger, IVehicleService vehicleService,
       IBookingService bookingService, IBookingDetailsService bookingDetailsService, IDriverService driverService) : BaseController
    {
        private readonly ILogger<VehicleController> _logger = logger;
        private readonly IDriverService _driverService = driverService;
        private readonly IVehicleService _vehicleService = vehicleService;
        private readonly IBookingService _bookingService = bookingService;
        private readonly IBookingDetailsService _bookingDetailsService = bookingDetailsService;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoadVehicleData([FromBody] DataTableAjaxPostModel request, CancellationToken token)
        {
            try
            {
                string search = "";
                if (!String.IsNullOrEmpty(request.search?.value))
                    search = request.search?.value ?? string.Empty;
                var result = await _vehicleService.GetVehicleListAsync(search, request.length, request.start, token);
                return Json(new
                {
                    draw = request.draw == 0 ? 1 : request.draw,
                    recordsFiltered = result.FilterRecords,
                    recordsTotal = result.TotalRecords,
                    data = result.VehicleDtos.AsParallel().ToArray()
                });
            }
            catch (Exception ex)
            {
                return Json("Something went wrong {0}", ex);
            }
        }

        public async Task<IActionResult> VehicleList(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View();
            }, token);
        }

        public async Task<IActionResult> AssignDriver(int VehicleId, CancellationToken token)
        {
            var drivers = await _driverService.GetUnAssignedDriversList(token);
            UnassignedDriversModel model = new()
            {
                UnassignedDrivers = [.. drivers.Select(x => new SelectListItem()
                {
                    Text = x.License,
                    Value = x.Id.ToString()
                })],
                VehicleId = VehicleId
            };
            return View(model);
        }
        public async Task<IActionResult> Edit(int VehicleId)
        {
            return null;
        }
        public async Task<IActionResult> Preview(int VehicleId)
        {
            return null;
        }
        public async Task<IActionResult> ExportAll(CancellationToken token)
        {
            return null;
        }
    }
}
