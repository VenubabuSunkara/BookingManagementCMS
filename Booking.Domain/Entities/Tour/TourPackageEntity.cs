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

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int? DurationDays { get; set; }

        public int? CategoryId { get; set; }

        public virtual TourPackageCategoryEntity? Category { get; set; }

        //public virtual ICollection<TourActivate> TourActivates { get; set; } = new List<TourActivate>();

        //public virtual ICollection<TourDestination> TourDestinations { get; set; } = new List<TourDestination>();

        //public virtual ICollection<TourGuideAssignment> TourGuideAssignments { get; set; } = new List<TourGuideAssignment>();

        //public virtual ICollection<TourItinerary> TourItineraries { get; set; } = new List<TourItinerary>();

        //public virtual ICollection<TourMediaGallery> TourMediaGalleries { get; set; } = new List<TourMediaGallery>();

        //public virtual ICollection<TourReview> TourReviews { get; set; } = new List<TourReview>();

    }

    public class PackageDTable
    {
        public int Total { get; set; }
        public int Filtered { get; set; }
        public IEnumerable<TourPackageEntity> PackageEntities { get; set; } = [];
    }
}
