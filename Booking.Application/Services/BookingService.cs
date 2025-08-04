using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class BookingService(IBookingRepository bookingRepository) : IBookingService
    {
        private readonly IBookingRepository _bookingRepository = bookingRepository;

        public async Task<IEnumerable<BookingOrderDto>> GetAllBookings(int VehicleId)
        {
            var bookings = await _bookingRepository.GetAllBookings(VehicleId);
            return bookings.Select(x => new BookingOrderDto()
            {
                VehicleId = x.VehicleId,
                BookingDate = x.BookingDate,
                Status = x.Status,
                CouponCodeId = x.CouponCodeId,
                PackageId = x.PackageId,
                CustomerId = x.CustomerId,
                TotalAmount = x.TotalAmount,
                TravelDate = x.TravelDate,
                Id = x.Id,
                BookingDetails = [.. x.BookingDetails.Select(y => new BookingDetailsDto()
                {
                    Id = y.Id,
                    PassengerAge = y.PassengerAge,
                    PassengerGender = y.PassengerGender,
                    PassengerName = y.PassengerName,
                    BookingId = y.BookingId,
                    RelativeId = y.RelativeId,
                })]
            }).AsParallel();
        }
    }
}
