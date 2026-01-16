using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class VehicleMediaMapping
{
    public int VehicleMediaMappingId { get; set; }

    public int MediaId { get; set; }

    public int VehicleId { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public Guid ItemGuid { get; set; }

    public virtual Medium Media { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
