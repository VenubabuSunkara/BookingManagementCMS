using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class DriverVehicleAssignment
{
    public int Id { get; set; }

    public int DriverId { get; set; }

    public int VehicleId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public int RouteId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime UpdatedOn { get; set; }

    public virtual Driver Driver { get; set; } = null!;

    public virtual DriverRoute Route { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
