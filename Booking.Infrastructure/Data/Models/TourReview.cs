using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class TourReview
{
    public int ItemId { get; set; }

    public Guid ItemGuid { get; set; }

    public int PackageId { get; set; }

    public int Rating { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public virtual TourPackage Package { get; set; } = null!;
}
