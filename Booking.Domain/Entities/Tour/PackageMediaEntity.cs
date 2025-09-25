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

        public string? MediaUrl { get; set; }
        public string? Filename { get; set; }

        public string? MediaType { get; set; }

        public bool? IsDefault { get; set; }

        public string? ThumbnailImage { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
