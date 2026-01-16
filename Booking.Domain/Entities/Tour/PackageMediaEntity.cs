using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities.Tour
{
    public class PackageMediaEntity
    {
        public int Id { get; set; }

        public int PackageId { get; set; }

        public string MediaUrl { get; set; } = null!;
        public string Filename { get; set; } = null!;

        public string MediaType { get; set; } = null!;

        public bool? IsDefault { get; set; }

        public string ThumbnailImage { get; set; } = null!;

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public string UpdatedBy { get; set; } = null!;

        public DateTime UpdatedAt { get; set; }
    }
}
