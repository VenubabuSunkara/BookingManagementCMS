using Booking.Application.DTOs;
using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Interfaces
{
    public interface IBookingService
    {
        Task<BookingOrderTableDto> GetCustomerBookings(string CustomerId, int Skip, int Take, CancellationToken token);
        Task<BookingOrderTableDto> GetVehicleBookings(int VehicleId, int Skip, int Take, CancellationToken token);
        Task<BookingOrderTableDto> GetDriverBookings(int DriverId, int Skip, int Take, CancellationToken token);
        Task<BookingOrderTableDto> GetAllBookings(int Skip, int Take, CancellationToken token, string searchKey = "");
    }
}
