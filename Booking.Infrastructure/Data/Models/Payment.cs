using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int BookingId { get; set; }

    public DateTime? PaymentDate { get; set; }

    public decimal? AmountPaid { get; set; }

    public decimal? TaxAmount { get; set; }

    public decimal? TotalAmount { get; set; }

    public decimal? RemainingAmount { get; set; }

    public string? PaymentMode { get; set; }

    public string? TransactionInfomation { get; set; }

    public string? PaymentStatus { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual BookingOrder Booking { get; set; } = null!;
}
