using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class ReviewComment
{
    public int Id { get; set; }

    public string? VehicleComment { get; set; }

    public decimal? Rating { get; set; }

    public int? DriverId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public int? VehicleId { get; set; }

    public string? DriverComment { get; set; }

    public string? Suggestion { get; set; }

    public virtual Driver? Driver { get; set; }

    public virtual Vehicle? Vehicle { get; set; }
}
