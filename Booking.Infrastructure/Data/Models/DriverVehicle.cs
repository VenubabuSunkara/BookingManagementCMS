using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class DriverVehicle
{
    public int DriverId { get; set; }

    public int VehicleId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public Guid ItemGuid { get; set; }

    public virtual Driver Driver { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
