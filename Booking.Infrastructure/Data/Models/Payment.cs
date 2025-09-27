using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int BookingOrderId { get; set; }

    public decimal Amount { get; set; }

    public string PaymentMode { get; set; } = null!;

    public DateTime PaymentDate { get; set; }

    public string Status { get; set; } = null!;

    public string? PaymentObject { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public Guid ItemGuid { get; set; }

    public virtual BookingOrder BookingOrder { get; set; } = null!;
}
