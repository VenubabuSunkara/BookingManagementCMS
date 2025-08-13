using Booking.Application.Interfaces;
using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    public class BookingsController : BaseController
    {
        private readonly ILogger<BookingsController> _logger;
        private readonly IDriverService _driverService;
        private readonly IBookingService _bookingService;
        private readonly IBookingDetailsService _bookingDetailsService;
        public BookingsController(ILogger<BookingsController> logger, IDriverService driverService,
          IBookingService bookingService, IBookingDetailsService bookingDetailsService)
        {
            _logger = logger;
            _driverService = driverService;
            _bookingService = bookingService;
            _bookingDetailsService = bookingDetailsService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetAllBookings([FromBody] DataTableAjaxPostModel request, CancellationToken cancellationToken)
        {
            var result = await _bookingService.GetAllBookings(request.start, request.length, "");
            return Json(new
            {
                draw = request.draw == 0 ? 1 : request.draw,
                recordsFiltered = result.FilterRecords,
                recordsTotal = result.TotalRecords,
                data = result.BookingsInfo.Select(x => new
                {
                    VehicleName = x.Vehicle.VehicleName,
                    DriverName = x.Driver.FullName,
                    BookingDate = x.BookingDate,
                    TravelDate = x.TravelDate,
                    Amount = x.TotalAmount,
                    PackageName = x.PackageId,
                    Id = x.Id
                }).ToArray()
            });
        }
    }
}
