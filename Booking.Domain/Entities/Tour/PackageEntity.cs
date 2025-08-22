using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities.Tour
{
    public class PackageEntity
    {
        public int ItemId { get; set; }

        public string PackageName { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int? DurationDays { get; set; }
        public TourPackageCategoryEntity TourPackageCategoryEntity { get; set; } = new TourPackageCategoryEntity();

        //public ICollection<TourActivate> TourActivates { get; set; } = new List<TourActivate>();

        //public ICollection<TourDestination> TourDestinations { get; set; } = new List<TourDestination>();

        //public TourGuideAssignment TourGuideAssignments { get; set; } = new TourGuideAssignment();

        //public ICollection<TourItinerary> TourItineraries { get; set; } = new List<TourItinerary>();

        //public ICollection<TourMediaGallery> TourMediaGalleries { get; set; } = new List<TourMediaGallery>();

        //public ICollection<TourReview> TourReviews { get; set; } = new List<TourReview>();

    }

    public class PackageDTable
    {
        public int Total { get; set; }
        public int Filtered { get; set; }
        public IEnumerable<PackageEntity> PackageEntities { get; set; } = [];
    }
}
