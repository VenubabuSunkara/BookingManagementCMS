using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class BookingDetailsEntity
    {
        public int Id { get; set; }

        public int BookingId { get; set; }

        public int? RelativeId { get; set; }

        public string? PassengerName { get; set; }

        public int? PassengerAge { get; set; }

        public string? PassengerGender { get; set; }
    }
}
