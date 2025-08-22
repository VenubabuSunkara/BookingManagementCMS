using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs.Tour
{
    public class PackageDto
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? ShortDescription { get; set; }

        public string? FullDescription { get; set; }

        public string? Source { get; set; }

        public string? Destination { get; set; }

        public int? DurationDays { get; set; }

        public decimal? Price { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string? TurmsandConditions { get; set; }

        public bool? IsActive { get; set; }

        public PackageMediaDto? PackageMedia { get; set; } = new PackageMediaDto();

    }
}
