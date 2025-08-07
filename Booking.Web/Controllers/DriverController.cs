using Booking.Application.Interfaces;
using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Booking.Web.Controllers
{
    public class DriverController : BaseController
    {
        private readonly ILogger<DriverController> _logger;
        private readonly IDriverService _driverService;

        public DriverController(ILogger<DriverController> logger, IDriverService driverService)
        {
            _logger = logger;
            _driverService = driverService;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoadDriverData(DataTableAjaxPostModel request, CancellationToken cancellationToken)
        {
            var result = await _driverService.GetDriverVehicleList(request.start, request.length);
            return Json(new
            {
                draw = request.draw == 0 ? 1 : request.draw,
                recordsFiltered = result.FilterRecords,
                recordsTotal = result.TotalRecords,
                data = result.DriverInfo.Select(x => new
                {
                    x.VehicleName,
                    x.SeatingCapacity,
                    Photo = x.VehicleThumbnail,
                    x.VehicleType,
                    x.DriverName,
                    Contact = x.DriverContact,
                    CreatedDate = x.Created,
                    x.DriverId
                }).ToArray()
            });
        }
        public async Task<int> Approve(int DriverVehicleId)
        {
            if (DriverVehicleId == 0)
                throw new ArgumentException();
            return await _driverService.ApproveDriverAsync(DriverVehicleId);
        }

        public async Task<IActionResult> Index()
        {
            return await Task.Run(() =>
            {
                return View();
            });
        }
    }
}
