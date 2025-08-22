using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class BookingOrder
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int PackageId { get; set; }

    public int? CouponCodeId { get; set; }

    public int? VehicleId { get; set; }

    public DateTime? BookingDate { get; set; }

    public DateTime? TravelDate { get; set; }

    public string? Status { get; set; }

    public decimal? TotalAmount { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? DriverId { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual CouponCode? CouponCode { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Driver? Driver { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Vehicle? Vehicle { get; set; }
}
