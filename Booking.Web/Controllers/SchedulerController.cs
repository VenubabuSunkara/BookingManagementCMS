using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Application.Services;
using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Controllers
{
    public class SchedulerController(ILogger<SchedulerController> logger,
        IDriverService driverService, IVehicleService vehicleService) : BaseController
    {
        private readonly ILogger<SchedulerController> _logger = logger;
        private readonly IDriverService _driverService = driverService;
        public readonly IVehicleService _vehicleService = vehicleService;
        public async Task<IActionResult> Index(CancellationToken token)
        {
            var drivers = await _driverService.GetDriversDropdownList(token);
            var vehicles = await _vehicleService.GetVehicleDropdownList(token);
            DriverVehicleScheduleDto model = new()
            {
                Drivers = [.. drivers.Select(x => new SelectListItem()
                {
                    Text = x.FullName,
                    Value = x.Id.ToString()
                })],
                Vehicles = [.. vehicles.Select(x => new SelectListItem()
                {
                    Text = $"{x.ModelName}-{x.RegistrationNumber}",
                    Value = x.VehicleId.ToString()
                })],
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                BookingStatus =
                [
                    new SelectListItem()
                    {
                        Value="Completed",
                        Text="Completed"
                    }, new SelectListItem()
                    {
                        Value="Canceled",
                        Text="Canceled"
                    },new SelectListItem()
                    {
                        Value="InProgress",
                        Text="InProgress"
                    }
                ]
            };
            return View(model);
        }
        public async Task<IActionResult> LoadScheduleData([FromBody] DataTableAjaxPostModel request, CancellationToken token)
        {
            return null;
        }
    }
}
