using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs.Tour
{
    public class LocationDto
    {
        public int LocationId { get; set; }

        public string Name { get; set; } = null!;

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }
    }
}
