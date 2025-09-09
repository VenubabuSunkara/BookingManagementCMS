using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class SeasonalPricing
{
    public int PricingId { get; set; }

    public int? VehicleId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public decimal Multiplier { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? ItemGuid { get; set; }

    public virtual Vehicle? Vehicle { get; set; }
}
