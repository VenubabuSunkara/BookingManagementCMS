using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class RentalContract
{
    public long Id { get; set; }

    public int BookingId { get; set; }

    public int VehicleId { get; set; }

    public decimal? RatePerHour { get; set; }

    public decimal? RatePerDay { get; set; }

    public DateTime StartAt { get; set; }

    public DateTime EndAt { get; set; }

    public string? Extras { get; set; }

    public string? Status { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual BookingOrder Booking { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
