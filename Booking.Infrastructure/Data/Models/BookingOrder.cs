using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Data.Models;

public partial class BookingOrder
{
    [Key]
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int PackageId { get; set; }

    public int? CouponCodeId { get; set; }

    public int? VehicleId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? BookingDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TravelDate { get; set; }

    [StringLength(20)]
    public string? Status { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? TotalAmount { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    [InverseProperty("Booking")]
    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    [ForeignKey("CouponCodeId")]
    [InverseProperty("BookingOrders")]
    public virtual CouponCode? CouponCode { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("BookingOrders")]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey("PackageId")]
    [InverseProperty("BookingOrders")]
    public virtual Package Package { get; set; } = null!;

    [InverseProperty("Booking")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
