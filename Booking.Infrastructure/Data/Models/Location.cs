using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class Location
{
    public int LocationId { get; set; }

    public Guid ItemGuid { get; set; }

    public string Name { get; set; } = null!;

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? ItemOrder { get; set; }

    public virtual ICollection<TourDestination> TourDestinations { get; set; } = new List<TourDestination>();
}
