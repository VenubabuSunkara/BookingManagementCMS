using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class DriverVehicleAvailability
{
    public int AvailabilityId { get; set; }

    public int DriverId { get; set; }

    public int VehicleId { get; set; }

    public DateOnly AvailableFrom { get; set; }

    public DateOnly AvailableTo { get; set; }

    public TimeOnly SlotStart { get; set; }

    public TimeOnly SlotEnd { get; set; }

    public bool IsFullDay { get; set; }

    public bool? IsAvailable { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedOn { get; set; }

    public Guid ItemGuid { get; set; }

    public virtual Driver Driver { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
