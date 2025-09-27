using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class TourBooking
{
    public long Id { get; set; }

    public int BookingId { get; set; }

    public int TourPackageId { get; set; }

    public int? Guests { get; set; }

    public decimal? Price { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual BookingOrder Booking { get; set; } = null!;

    public virtual TourPackage TourPackage { get; set; } = null!;
}
