using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task<BookingOrderTableEntity> GetCustomerBookings(string CustomerId, int Skip, int Take, CancellationToken token);
        Task<BookingOrderTableEntity> GetVehicleBookings(int VehicleId, int DriverId, int Skip, int Take, CancellationToken token);
        Task<BookingOrderTableEntity> GetDriverBookings(int DriverId, int Skip, int Take, CancellationToken token);
        Task<BookingOrderTableEntity> GetAllBookings(int Skip, int Take, CancellationToken token, string searchKey = "");
    }
}
