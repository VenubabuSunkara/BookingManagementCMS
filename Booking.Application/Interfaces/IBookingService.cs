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
        Task<IEnumerable<BookingOrderDto>> GetAllBookings(int VehicleId);
        Task<IEnumerable<BookingOrderDto>> GetAllBookings(int VehicleId, int Year);
        Task<BookingsDataTableDto> GetAllBookings(int Skip, int Take, string searchKey = "");
    }
}
