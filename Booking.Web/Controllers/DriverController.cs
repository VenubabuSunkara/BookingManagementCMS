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
        //public async Task<IActionResult> LoadDriverData(DatatableRequest request, CancellationToken cancellationToken)
        //{
        //    var result = await _driverService.GetAllAsync();
        //    return Json(new
        //    {
        //        draw = result.draw,
        //        recordsFiltered = result.recordsFiltered,
        //        recordsTotal = result.recordsTotal,
        //        data = result?.data?.Select(x => new
        //        {
        //            x.VehicleName,
        //            x.Capacity,
        //            Photo = x.DriverVehicleMedia
        //                     .FirstOrDefault(m => m.IsDefault)?
        //                     .MediaValue,
        //            VehicleType = x.VehicleType,
        //            DriverName = x.DriverFirstName + " " + x.DriverLastName,
        //            Contact = x.DriverContact,
        //            CreatedDate = x.CreatedOn,
        //            x.Id
        //        }).ToArray()
        //    });
        //}
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
