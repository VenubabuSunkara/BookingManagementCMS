using Booking.Application.Interfaces;
using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    public class BookingsController : BaseController
    {
        private readonly ILogger<BookingsController> _logger;
        private readonly IBookingService _bookingService;
        public BookingsController(ILogger<BookingsController> logger, IBookingService bookingService)
        {
            _logger = logger;
            _bookingService = bookingService;
        }
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() =>
            {
                return View();
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetAllBookings([FromBody] DataTableAjaxPostModel request,
            CancellationToken cancellationToken)
        {
            string search = "";
            if (!String.IsNullOrEmpty(request.search?.value))
                search = request.search?.value ?? string.Empty;
            var result = await _bookingService.GetAllBookings(request.start, request.length, cancellationToken, search);
            return Json(new
            {
                draw = request.draw == 0 ? 1 : request.draw,
                recordsFiltered = result.FilterRecords,
                recordsTotal = result.TotalRecords,
                data = result.BookingOrders
            });
        }
    }
}
