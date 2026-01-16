using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class BookingOrder
{
    public int BookingOrderId { get; set; }

    public string BookingNumber { get; set; } = null!;

    public int CustomerId { get; set; }

    public int DriverId { get; set; }

    public int VehicleId { get; set; }

    public DateTime BookingDate { get; set; }

    public string PickupLocation { get; set; } = null!;

    public string DropLocation { get; set; } = null!;

    public string TripType { get; set; } = null!;

    public string Status { get; set; } = null!;

    public decimal? EstimatedFare { get; set; }

    public decimal? ActualFare { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public DateTime? ScheduledPickupTime { get; set; }

    public DateTime? ScheduledDropTime { get; set; }

    public DateTime CreatedOn { get; set; }

    public decimal? DistanceInKm { get; set; }

    public decimal? ExtraCharges { get; set; }

    public int? DurationInMin { get; set; }

    public int? CoupenId { get; set; }

    public decimal? DiscountAmount { get; set; }

    public decimal? TaxAmount { get; set; }

    public Guid ItemGuid { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual Customer Customer { get; set; } = null!;

    public virtual Driver Driver { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Vehicle Vehicle { get; set; } = null!;
}
