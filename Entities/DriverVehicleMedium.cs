using System;
using System.Collections.Generic;

namespace Entities;

public partial class DriverVehicleMedium
{
    public int Id { get; set; }

    public string MediaName { get; set; } = null!;

    public string MediaValue { get; set; } = null!;

    public int DriverVehicleId { get; set; }

    public bool IsActive { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsDefault { get; set; }

    public string? Thumbnail { get; set; }

    public virtual DriverAndVehicle DriverVehicle { get; set; } = null!;
}
