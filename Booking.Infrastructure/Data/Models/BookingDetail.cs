using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class BookingDetail
{
    public int BookingDetailId { get; set; }

    public int BookingOrderId { get; set; }

    public decimal? ExtraCharges { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid ItemGuid { get; set; }

    public virtual BookingOrder BookingOrder { get; set; } = null!;
}
