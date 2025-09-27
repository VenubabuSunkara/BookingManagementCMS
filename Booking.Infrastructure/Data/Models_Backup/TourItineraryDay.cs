using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class TourItineraryDay
{
    public int ItemId { get; set; }

    public Guid ItemGuid { get; set; }

    public int PackageId { get; set; }

    public int DayNumber { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public DateOnly? Date { get; set; }

    public virtual TourPackage Package { get; set; } = null!;
}
