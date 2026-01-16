using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class TourPackageMedium
{
    public int MediaId { get; set; }

    public Guid ItemGuid { get; set; }

    public int PackageId { get; set; }

    public string MediaUrl { get; set; } = null!;

    public string MediaType { get; set; } = null!;

    public string Caption { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public string? ThumbnailUrl { get; set; }

    public virtual TourPackage Package { get; set; } = null!;
}
