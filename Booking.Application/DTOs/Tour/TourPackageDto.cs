using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs.Tour
{
    public class TourPackageDto
    {
        public int Id { get; set; }
        public string PackageName { get; set; }
        public string? ShortDescription { get; set; }
        public string? FullDescription { get; set; }
        public string? Source { get; set; }
        public string? BannerImage { get; set; }
        public string Destination { get; set; }
        [MaxLength(50)]
        public string DurationDays { get; set; } 
        public decimal Price { get; set; }
        public string? ThingsToNote { get; set; }
        public string? Inclusions { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? CategoryId { get; set; }
        public Guid ItemGuid { get; set; } = Guid.NewGuid();
    }
}
