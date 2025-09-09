using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class VehicleRating
{
    public int RatingId { get; set; }

    public int? VehicleId { get; set; }

    public int? PassengerId { get; set; }

    public int? Rating { get; set; }

    public string? Comments { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? ItemGuid { get; set; }

    public virtual Customer? Passenger { get; set; }

    public virtual Vehicle? Vehicle { get; set; }
}
