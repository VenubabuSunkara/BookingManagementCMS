using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class Airport
{
    public int Id { get; set; }

    public string? Iata { get; set; }

    public string? Name { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? DefaultPickupZone { get; set; }

    public string? DefaultDropoffZone { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
