
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class MediaDto
    {
        public int MediaId { get; set; }

        public string MediaName { get; set; } = null!;

        public string MediaUrl { get; set; } = null!;

        public string MediaType { get; set; } = null!;
        public string? ThumbnailUrl { get; set; }
    }
}
