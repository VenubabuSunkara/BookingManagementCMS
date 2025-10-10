using Booking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Booking.Web.ViewComponents
{
    public class BookingHistoryViewComponent : ViewComponent
    {
        private readonly ILogger<BookingHistoryViewComponent> _logger;
        private readonly IBookingService _bookingService;
        public BookingHistoryViewComponent(ILogger<BookingHistoryViewComponent> logger, IBookingService bookingService)
        {
            _logger = logger;
            _bookingService = bookingService;
        }
        public async Task<IViewComponentResult> InvokeAsync(CancellationToken token)
        {

            if (HttpContext.User.Identity?.IsAuthenticated == true)
            {
                var bookings = await _bookingService.GetAllBookings(0, 10, token);
                return View(bookings.BookingOrders);
            }
            return View("Unauthorized");
        }
    }
}
