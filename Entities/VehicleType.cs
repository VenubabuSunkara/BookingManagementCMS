using System;
using System.Collections.Generic;

namespace Entities;

public partial class VehicleType
{
    public int Id { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string VehicleType1 { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual ICollection<DriverAndVehicle> DriverAndVehicles { get; set; } = new List<DriverAndVehicle>();
}
