using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class BookingDetail
{
    public int Id { get; set; }

    public int BookingId { get; set; }

    public int? RelativeId { get; set; }

    public string? PassengerName { get; set; }

    public int? PassengerAge { get; set; }

    public string? PassengerGender { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual BookingOrder Booking { get; set; } = null!;

    public virtual CustomerRelative? Relative { get; set; }
}
