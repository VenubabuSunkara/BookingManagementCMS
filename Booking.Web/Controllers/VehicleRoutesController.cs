using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Controllers
{
    public class VehicleRoutesController(ILogger<VehicleRoutesController> logger,
        IDriverService driverService, IVehicleService vehicleService) : BaseController
    {
        private readonly ILogger<VehicleRoutesController> _logger = logger;
        private readonly IDriverService _driverService = driverService;
        public readonly IVehicleService _vehicleService = vehicleService;
        public async Task<IActionResult> Index(int? VehicleId, int? Driverid, CancellationToken token)
        {
            var drivers = await _driverService.GetDriversDropdownList(token);
            var vehicles = await _vehicleService.GetVehicleDropdownList(token);
            DriverVehicleScheduleDto model = new()
            {
                DriverId = Driverid ?? 0,
                VehicleId = VehicleId ?? 0,
                //Drivers = [.. drivers.Select(x => new SelectListItem()
                //{
                //    Text = x.FullName,
                //    Value = x.Id.ToString()
                //})],
                //Vehicles = [.. vehicles.Select(x => new SelectListItem()
                //{
                //    Text = $"{x.ModelName}-{x.RegistrationNumber}",
                //    Value = x.VehicleId.ToString()
                //})]
            };
            return View(model);
        }
    }
}
