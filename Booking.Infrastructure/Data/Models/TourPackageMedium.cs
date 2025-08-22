using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class TourPackageMedium
{
    public int MediaId { get; set; }

    public Guid ItemGuid { get; set; }

    public int PackageId { get; set; }

    public string MediaUrl { get; set; } = null!;

    public string? MediaType { get; set; }

    public string? Caption { get; set; }

    public int? SequenceOrder { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? ItemOrder { get; set; }

    public virtual TourPackage Package { get; set; } = null!;
}
