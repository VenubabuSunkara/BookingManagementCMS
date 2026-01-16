using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class ReviewComment
{
    public int Id { get; set; }

    public string Comment { get; set; } = null!;

    public decimal Rating { get; set; }

    public int DriverId { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public Guid ItemGuid { get; set; }

    public virtual Driver Driver { get; set; } = null!;
}
