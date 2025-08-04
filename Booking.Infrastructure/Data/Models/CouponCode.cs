using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Data.Models;

public partial class CouponCode
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Code { get; set; }

    public DateOnly? ValidityFrom { get; set; }

    public DateOnly? ValidityTo { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? PriceRangeMin { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? PriceRangeMax { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? DiscountType { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? DiscountValue { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public string? MediaUrl { get; set; }

    [InverseProperty("CouponCode")]
    public virtual ICollection<BookingOrder> BookingOrders { get; set; } = new List<BookingOrder>();
}
