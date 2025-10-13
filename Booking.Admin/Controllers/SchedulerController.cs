using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Application.Services;
using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Controllers
{
    public class SchedulerController(ILogger<SchedulerController> logger,
        IDriverService driverService, IVehicleService vehicleService, IDriverVehicleAvailabilityService availabilityService) : BaseController
    {
        private readonly ILogger<SchedulerController> _logger = logger;
        private readonly IDriverService _driverService = driverService;
        private readonly IVehicleService _vehicleService = vehicleService;
        private readonly IDriverVehicleAvailabilityService _availabilityService = availabilityService;
        public async Task<IActionResult> Index(int? VehicleId, int? Driverid, CancellationToken token)
        {
            var drivers = await _driverService.GetDriversDropdownList(token);
            var vehicles = await _vehicleService.GetVehicleDropdownList(token);
            DriverScheduleViewModel model = new()
            {
                DriverId = Driverid ?? 0,
                VehicleId = VehicleId ?? 0,
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
                //StartDate = DateTime.Now,
                //EndDate = DateTime.Now,
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
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid request data." });
            }

            try
            {
                string search = "";
                if (!String.IsNullOrEmpty(request.search?.value))
                    search = request.search?.value ?? string.Empty;

                var schedules = await _availabilityService.DriverVehicleSchedulesList(request.search.value, request.length, request.start, token);
                return Json(new
                {
                    draw = request.draw == 0 ? 1 : request.draw,
                    recordsFiltered = schedules.FilterRecords,
                    recordsTotal = schedules.TotalRecords,
                    data = schedules.DriverVehicleSchedules.AsParallel().ToArray()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading schedule data");
                return Json("Something went wrong {0}", ex);
            }
        }
    }
}
