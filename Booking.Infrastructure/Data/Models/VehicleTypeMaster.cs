using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class VehicleTypeMaster
{
    public int VehicleTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public string? Description { get; set; }

    public int? Seats { get; set; }

    public int? LuggageCapacity { get; set; }

    public string? FuelType { get; set; }

    public string? Transmission { get; set; }

    public bool? AirConditioning { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
