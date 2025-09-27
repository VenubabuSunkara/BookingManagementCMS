using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class TourActivate
{
    public int ItemId { get; set; }

    public Guid ItemGuid { get; set; }

    public int PackageId { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string? Description { get; set; }

    public virtual TourPackage Package { get; set; } = null!;
}
