using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class TourGuideAssignment
{
    public int ItemId { get; set; }

    public int GuideId { get; set; }

    public int PackageId { get; set; }

    public DateOnly? AssignmentDate { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public Guid ItemGuid { get; set; }

    public virtual TourGuide Guide { get; set; } = null!;

    public virtual TourPackage Package { get; set; } = null!;
}
