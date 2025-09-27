using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class VehicleFeatureMapping
{
    public int VehicleId { get; set; }

    public int FeatureId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public Guid ItemGuid { get; set; }

    public virtual VehicleFeature Feature { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
