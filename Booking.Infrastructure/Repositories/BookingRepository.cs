using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repositories
{
    public class BookingRepository(BookingCmsContext context) : IBookingRepository
    {
        private readonly BookingCmsContext _context = context;
        public async Task<IEnumerable<BookingOrder>> GetAllBookings(int VehicleId)
        {
            var bookings = await _context.BookingOrders.Include(x => x.BookingDetails)
                                         .Where(x => x.VehicleId.Equals(VehicleId))
                                         .ToListAsync();
            return bookings.Select(x => new BookingOrder
            {
                BookingDate = x.BookingDate,
                TravelDate = x.TravelDate,
                CouponCodeId = x.CouponCodeId,
                CustomerId = x.CustomerId,
                Id = x.Id,
                PackageId = x.PackageId,
                Status = x.Status,
                TotalAmount = x.TotalAmount,
                VehicleId = x.VehicleId,
                BookingDetails = [.. x.BookingDetails.Select(y => new BookingDetailsEntity()
                {
                    BookingId = y.BookingId,
                    Id = y.Id,
                    PassengerName = y.PassengerName,
                    PassengerAge = y.PassengerAge,
                    PassengerGender = y.PassengerGender,
                    RelativeId = y.RelativeId,
                })]
            }).AsParallel();
        }
        public async Task<IEnumerable<BookingOrder>> GetAllBookings(int VehicleId, int Year)
        {
            var bookings = await _context.BookingOrders.Include(x => x.BookingDetails)
                                         .Where(x => x.VehicleId.Equals(VehicleId) && x.CreatedOn.Value.Year == Year)
                                         .ToListAsync();
            return bookings.Select(x => new BookingOrder
            {
                BookingDate = x.BookingDate,
                TravelDate = x.TravelDate,
                CouponCodeId = x.CouponCodeId,
                CustomerId = x.CustomerId,
                Id = x.Id,
                PackageId = x.PackageId,
                Status = x.Status,
                TotalAmount = x.TotalAmount,
                VehicleId = x.VehicleId,
                BookingDetails = [.. x.BookingDetails.Select(y => new BookingDetailsEntity()
                {
                    BookingId = y.BookingId,
                    Id = y.Id,
                    PassengerName = y.PassengerName,
                    PassengerAge = y.PassengerAge,
                    PassengerGender = y.PassengerGender,
                    RelativeId = y.RelativeId,
                })]
            }).AsParallel();
        }
    }
}
