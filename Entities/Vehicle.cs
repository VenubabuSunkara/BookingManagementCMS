using System;
using System.Collections.Generic;

namespace Entities;

public partial class Vehicle
{
    public int Id { get; set; }

    public string VehicleNumber { get; set; } = null!;

    public int VehicleTypeId { get; set; }

    public int DriverId { get; set; }

    public int Capacity { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsAvailable { get; set; }

    public string? TurmsandConditions { get; set; }

    public string? Features { get; set; }

    public virtual ICollection<DriverDetail> DriverDetails { get; set; } = new List<DriverDetail>();

    public virtual ICollection<VehicleMedium> VehicleMedia { get; set; } = new List<VehicleMedium>();

    public virtual VehicleType VehicleType { get; set; } = null!;
}
