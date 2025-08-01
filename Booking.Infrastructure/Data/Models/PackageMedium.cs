using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class PackageMedium
{
    public int Id { get; set; }

    public int PackageId { get; set; }

    public string? MediaUrl { get; set; }

    public string? MediaType { get; set; }

    public bool? IsDefault { get; set; }

    public string? ThumbnailImage { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Package Package { get; set; } = null!;
}
