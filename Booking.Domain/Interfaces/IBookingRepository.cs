using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task<IEnumerable<BookingOrder>> GetAllBookings(int VehicleId);
        Task<IEnumerable<BookingOrder>> GetAllBookings(int VehicleId, int Year);
        Task<BookingsDTable> GetAllBookings(int Skip, int Take, string searchKey = "");
    }
}
