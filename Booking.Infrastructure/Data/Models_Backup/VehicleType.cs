using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class VehicleType
{
    public int ItemId { get; set; }

    public Guid ItemGuid { get; set; }

    public string TypeName { get; set; } = null!;

    public string? Description { get; set; }

    public int OrderId { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime UpdatedOn { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
