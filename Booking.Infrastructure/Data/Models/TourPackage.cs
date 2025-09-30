using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class TourPackage
{
    public int ItemId { get; set; }

    public Guid ItemGuid { get; set; }

    public string PackageName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal BasePrice { get; set; }

    public string DurationDays { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public int CategoryId { get; set; }

    public string BannerImage { get; set; } = null!;

    public string? ShortDescription { get; set; }
    public string? ThingsToNote { get; set; }
    public string? Inclusions { get; set; }

    public virtual TourPackageCategory Category { get; set; } = null!;

    public virtual ICollection<TourActivate> TourActivates { get; set; } = new List<TourActivate>();

    public virtual ICollection<TourGuideAssignment> TourGuideAssignments { get; set; } = new List<TourGuideAssignment>();

    public virtual ICollection<TourLocation> TourLocations { get; set; } = new List<TourLocation>();

    public virtual ICollection<TourPackageItineraryDay> TourPackageItineraryDays { get; set; } = new List<TourPackageItineraryDay>();

    public virtual ICollection<TourPackageMedium> TourPackageMedia { get; set; } = new List<TourPackageMedium>();

    public virtual ICollection<TourPackagePolicy> TourPackagePolicies { get; set; } = new List<TourPackagePolicy>();

    public virtual ICollection<TourReview> TourReviews { get; set; } = new List<TourReview>();
}
