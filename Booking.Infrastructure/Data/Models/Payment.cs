using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Data.Models;

public partial class Payment
{
    [Key]
    public int Id { get; set; }

    [Column("BookingID")]
    public int BookingId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PaymentDate { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? AmountPaid { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? TaxAmount { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? TotalAmount { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? RemainingAmount { get; set; }

    [StringLength(20)]
    public string? PaymentMode { get; set; }

    public string? TransactionInfomation { get; set; }

    [StringLength(20)]
    public string? PaymentStatus { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    [ForeignKey("BookingId")]
    [InverseProperty("Payments")]
    public virtual BookingOrder Booking { get; set; } = null!;
}
