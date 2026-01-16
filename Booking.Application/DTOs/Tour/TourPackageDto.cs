using Booking.Domain.Entities.Tour;
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

        //TODO: Add validation attributes
        //NO of Persons from family allowing 
        //Seperate Days and Nights as two fields
        public int? NoDays { get; set; }
        public int? NoNights { get; set; }
        public int? NoPasangers { get; set; }
        public int? Rating { get; set; }
        //
        public int Id { get; set; }
        public string PackageName { get; set; } = null!;
        public string? ShortDescription { get; set; }
        public string? FullDescription { get; set; }
        public string? Source { get; set; }
        public string BannerImage { get; set; } = null!;
        [MaxLength(50)]
        public string DurationDays { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ThingsToNote { get; set; }
        public string? Inclusions { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string UpdatedBy { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int CategoryId { get; set; }
        public Guid ItemGuid { get; set; } = Guid.NewGuid();
        public TourPackageCategoryDto Category { get; set; } = new TourPackageCategoryDto();
        public TourLocationDto Location { get; set; } = new TourLocationDto();
        public List<PackageMediaDto> PackageMedia { get; set; } = [];
        
    }
}
