using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class DriverVehicleAvailability
{
    public int AvailabilityId { get; set; }

    public int? DriverId { get; set; }

    public int? VehicleId { get; set; }

    public DateOnly AvailableDate { get; set; }

    public TimeOnly SlotStart { get; set; }

    public TimeOnly SlotEnd { get; set; }

    public bool? IsAvailable { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? ItemGuid { get; set; }

    public virtual Driver? Driver { get; set; }

    public virtual Vehicle? Vehicle { get; set; }
}
