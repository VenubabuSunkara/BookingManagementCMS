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
    public class BookingDetailsService(IBookingDetailsRepository bookingDetailsRepository) : IBookingDetailsService
    {
        private readonly IBookingDetailsRepository _bookingDetailsRepository = bookingDetailsRepository;

        public async Task<IEnumerable<BookingDetailsDto>> GetAllBookingDetailsAsync(int BookingId)
        {
            var bookingDetails = await _bookingDetailsRepository.GetAllBookingDetailsAsync(BookingId);
            return bookingDetails.Select(x => new BookingDetailsDto()
            {
                BookingId=x.BookingId,
                PassengerName=x.PassengerName,
                PassengerAge=x.PassengerAge,
                PassengerGender=x.PassengerGender,
                RelativeId=x.RelativeId,
            }).AsParallel();
        }
    }
}
