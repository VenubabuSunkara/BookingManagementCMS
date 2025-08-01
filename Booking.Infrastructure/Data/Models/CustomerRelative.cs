using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class CustomerRelative
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string? FullName { get; set; }

    public int Age { get; set; }

    public string Gender { get; set; } = null!;

    public string? Relationship { get; set; }

    public string? PhoneNumber { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual Customer Customer { get; set; } = null!;
}
