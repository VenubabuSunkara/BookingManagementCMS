using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class TourMediaGallery
{
    public int ItemId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public Guid ItemGuid { get; set; }

    public int PackageId { get; set; }

    public string? MediaType { get; set; }

    public string? MediaUrl { get; set; }

    public virtual TourPackage Package { get; set; } = null!;
}
