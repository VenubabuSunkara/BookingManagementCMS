using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class DriverVehicleMapping
{
    public int Id { get; set; }

    public int? DriverId { get; set; }

    public int? VehicleId { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual Driver Driver { get; set; } = new Driver();

    public virtual Vehicle Vehicle { get; set; } = new Vehicle();
}
