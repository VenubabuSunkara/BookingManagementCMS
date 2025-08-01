using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class Package
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? ShortDescription { get; set; }

    public string? FullDescription { get; set; }

    public string? Source { get; set; }

    public string? Destination { get; set; }

    public int? DurationDays { get; set; }

    public decimal? Price { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? TurmsandConditions { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<BookingOrder> BookingOrders { get; set; } = new List<BookingOrder>();

    public virtual ICollection<PackageMedium> PackageMedia { get; set; } = new List<PackageMedium>();
}
