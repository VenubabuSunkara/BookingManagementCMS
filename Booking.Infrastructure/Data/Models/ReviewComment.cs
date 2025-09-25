using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class ReviewComment
{
    public int Id { get; set; }

    public string? Comment { get; set; }

    public decimal? Rating { get; set; }

    public int? DriverId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }
}
