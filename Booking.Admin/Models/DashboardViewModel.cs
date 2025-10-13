using Booking.Application.DTOs;
using Booking.Application.DTOs.Tour;

namespace Booking.Web.Models
{
    public class DashboardViewModel
    {
        public int TotalCustomers { get; set; }
        public int TotalBookings { get; set; }
        public int TotalReviews { get; set; }
        public int TotalPackages { get; set; }
        public IEnumerable<BookingDetailsDto> Bookings { get; set; }= [];
        public IEnumerable<ReviewCommentsDto> Reviews { get; set; }= [];
        public IEnumerable<TourPackageDto> Packages { get; set; }= [];
    }
}
