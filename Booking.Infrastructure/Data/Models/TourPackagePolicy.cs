using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class TourPackagePolicy
{
    public int PolicyId { get; set; }

    public Guid ItemGuid { get; set; }

    public int PackageId { get; set; }

    public string PolicyType { get; set; } = null!;

    public string? Title { get; set; }

    public string Description { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? ItemOrder { get; set; }

    public string? PolicyDocumnetUrls { get; set; }

    public virtual TourPackage Package { get; set; } = null!;
}
