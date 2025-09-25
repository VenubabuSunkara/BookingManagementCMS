using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities.Tour
{
    public class TourPackageEntity
    {
        public int ItemId { get; set; }
        public Guid ItemGuid { get; set; }
        public string PackageName { get; set; } = null!;
        public string? FullDescription { get; set; }
        public string? ShortDescription { get; set; }
        public decimal BasePrice { get; set; }
        public required string DurationDays { get; set; }
        public string? BannerImage { get; set; }
        public int? CategoryId { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? ThingsToNote { get; set; }
        public string? Inclusions { get; set; }
        public TourPackageCategoryEntity Category { get; set; } = new TourPackageCategoryEntity();
        public LocationEntity Location { get; set; } = new LocationEntity();
    }

    public class TourPackageTable
    {
        public int Total { get; set; }
        public int Filtered { get; set; }
        public IEnumerable<TourPackageEntity> PackageEntities { get; set; } = [];
    }
}
