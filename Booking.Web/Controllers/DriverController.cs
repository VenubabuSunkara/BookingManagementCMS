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
                draw = request.draw,
                recordsFiltered = result.recordsFiltered,
                recordsTotal = result.recordsTotal,
                data = result?.Select(x => new
                {
                    x.VehicleName,
                    x.SeatingCapacity,
                    Photo = x.VehicleThumbnail,
                    VehicleType = x.VehicleType,
                    DriverName = x.DriverName,
                    Contact = x.DriverContact,
                    CreatedDate = x.CreatedOn,
                    x.DriverId
                }).ToArray()
            });
        }
        public async Task<int> Approve(int DriverVehileId)
        {
            if (DriverVehileId == 0)
                throw new ArgumentException();
            return await _driverService.ApproveDriverAsync(DriverVehileId);
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
