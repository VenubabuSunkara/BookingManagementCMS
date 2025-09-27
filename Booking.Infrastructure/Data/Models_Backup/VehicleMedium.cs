using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class VehicleMedium
{
    public int Id { get; set; }

    public string MediaName { get; set; } = null!;

    public string MediaUrl { get; set; } = null!;

    public string MediaType { get; set; } = null!;

    public int VehicleId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsDefault { get; set; }

    public string? ThumbnailUrl { get; set; }

    public virtual Vehicle Vehicle { get; set; } = null!;
}
