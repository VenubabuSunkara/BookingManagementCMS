using System;
using System.Collections.Generic;

namespace Entities;

public partial class VehicleMedium
{
    public int Id { get; set; }

    public string MediaType { get; set; } = null!;

    public string MediaUrl { get; set; } = null!;

    public int VehicleId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsAvailable { get; set; }

    public string MediaFileName { get; set; } = null!;

    public string? MediaDescription { get; set; }

    public string? MediaWhom { get; set; }

    public virtual Vehicle Vehicle { get; set; } = null!;
}
