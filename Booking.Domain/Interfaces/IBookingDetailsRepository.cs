using Booking.Domain.Entities;

namespace Booking.Domain.Interfaces
{
    public interface IBookingDetailsRepository
    {
        Task<IEnumerable<BookingDetailsEntity>> GetAllBookingDetailsAsync(int BookingId);
    }
}
