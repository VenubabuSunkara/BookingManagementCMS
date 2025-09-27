using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class BookingDetail
{
    public int BookingDetailId { get; set; }

    public int BookingOrderId { get; set; }

    public string? StopLocation { get; set; }

    public decimal? DistanceInKm { get; set; }

    public int? DurationInMin { get; set; }

    public decimal? ExtraCharges { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual BookingOrder BookingOrder { get; set; } = null!;
}
