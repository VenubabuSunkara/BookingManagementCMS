using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class VehicleMedia
    {
        public int Id { get; set; }

        public string MediaName { get; set; } = null!;

        public string MediaUrl { get; set; } = null!;

        public string MediaType { get; set; } = null!;

        public int VehicleId { get; set; }

        public bool IsDefault { get; set; }

        public string? ThumbnailUrl { get; set; }
    }
}
