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
    public class BookingDetailsRepository(BookingCmsContext context) : IBookingDetailsRepository
    {
        private readonly BookingCmsContext _context = context;
        public async Task<IEnumerable<BookingDetailsEntity>> GetAllBookingDetailsAsync(int BookingId)
        {
            var BookingDetails = await _context.BookingDetails.Where(x => x.Equals(BookingId)).ToListAsync();
            return BookingDetails.Select(x => new BookingDetailsEntity()
            {
                PassengerName = x.PassengerName,
                BookingId = x.BookingId,
                Id = x.BookingId,
                PassengerAge = x.PassengerAge,
                PassengerGender = x.PassengerGender,
                RelativeId = x.RelativeId,
            }).AsParallel();
        }
    }
}
