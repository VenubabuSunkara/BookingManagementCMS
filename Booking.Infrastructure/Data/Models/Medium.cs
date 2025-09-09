using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class Medium
{
    public int MediaId { get; set; }

    public string MediaName { get; set; } = null!;

    public string MediaUrl { get; set; } = null!;

    public string MediaType { get; set; } = null!;

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public string? ThumbnailUrl { get; set; }

    public Guid? ItemGuid { get; set; }

    public virtual ICollection<DriverMediaMapping> DriverMediaMappings { get; set; } = new List<DriverMediaMapping>();

    public virtual ICollection<VehicleMediaMapping> VehicleMediaMappings { get; set; } = new List<VehicleMediaMapping>();
}
