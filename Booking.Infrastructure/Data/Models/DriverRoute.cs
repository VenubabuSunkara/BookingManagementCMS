using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class DriverRoute
{
    public int Id { get; set; }

    public string StartLocation { get; set; } = null!;

    public string EndLocation { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public double Distance { get; set; }

    public string Status { get; set; } = null!;

    public bool IsActive { get; set; }

    public int DriverId { get; set; }

    public int VehicleId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime UpdatedOn { get; set; }

    public string? RouteName { get; set; }

    public virtual Driver Driver { get; set; } = null!;

    public virtual ICollection<DriverVehicleAssignment> DriverVehicleAssignments { get; set; } = new List<DriverVehicleAssignment>();

    public virtual Vehicle Vehicle { get; set; } = null!;
}
