using Booking.Application.Interfaces;
using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Booking.Web.Controllers
{
    public class DriverController : BaseController
    {
        /*
  * Required Actions 
  * 1. Get All Drivers With Pagination and search  -- Super admin   -- Done
  * 2. Approve Driver  --- Super admin   -- Done
  * 3. Update Driver Availability Schedule  -- Super admin and Driver
  * 4. Update Driver Details -- Driver
  * 5. Update Vehicle Details  --driver
  * 6. View Bookings  -- Driver and super admin  -- 
  * 7. View Orders -- Driver and super admin
  * 8. View Reviews -- driver and super admin
  * 9. InActive/DeActivate
  * 10. Export  -- Super admin
  * 11. Import Vehicle and Driver -- super admin
  * 12. Bulk delete -- super admin
  * 13. Transfer Schedule to other driver -- super admin
  */
        private readonly ILogger<DriverController> _logger;
        private readonly IDriverService _driverService;
        private readonly IBookingService _bookingService;
        private readonly IBookingDetailsService _bookingDetailsService;
        public DriverController(ILogger<DriverController> logger, IDriverService driverService,
            IBookingService bookingService, IBookingDetailsService bookingDetailsService)
        {
            _logger = logger;
            _driverService = driverService;
            _bookingService = bookingService;
            _bookingDetailsService = bookingDetailsService;
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> LoadDriverData([FromBody] DataTableAjaxPostModel request, CancellationToken cancellationToken)
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
       // public async Task<IActionResult> GetOrders()
    }
}
