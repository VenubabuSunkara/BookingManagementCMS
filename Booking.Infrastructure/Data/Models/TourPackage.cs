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

    public int? DurationDays { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public int? CategoryId { get; set; }

    public string? BannerImage { get; set; }

    public virtual TourPackageCategory? Category { get; set; }

    public virtual ICollection<PackagePolicy> PackagePolicies { get; set; } = new List<PackagePolicy>();

    public virtual ICollection<TourActivate> TourActivates { get; set; } = new List<TourActivate>();

    public virtual ICollection<TourDestination> TourDestinations { get; set; } = new List<TourDestination>();

    public virtual ICollection<TourGuideAssignment> TourGuideAssignments { get; set; } = new List<TourGuideAssignment>();

    public virtual ICollection<TourItineraryDay> TourItineraryDays { get; set; } = new List<TourItineraryDay>();

    public virtual ICollection<TourPackageItem> TourPackageItems { get; set; } = new List<TourPackageItem>();

    public virtual ICollection<TourPackageMedium> TourPackageMedia { get; set; } = new List<TourPackageMedium>();

    public virtual ICollection<TourPackagePolicy> TourPackagePolicies { get; set; } = new List<TourPackagePolicy>();

    public virtual ICollection<TourReview> TourReviews { get; set; } = new List<TourReview>();
}
