using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class TourLocation
{
    public Guid ItemGuid { get; set; }

    public int LocationId { get; set; }

    public string? ViaLocations { get; set; }

    public string LocationHeadLine { get; set; } = null!;

    public string LocationName { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string? State { get; set; }

    public string? City { get; set; }

    public string ZipCode { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Latitude { get; set; } = null!;

    public string Longitude { get; set; } = null!;

    public string? Description { get; set; }

    public string? PointImage { get; set; }

    public int RouteDistance { get; set; }

    public int RouteDuration { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedOn { get; set; }

    public bool? IsActive { get; set; }

    public int? SortOrder { get; set; }

    public int PackageId { get; set; }

    public string FullAddress { get; set; } = null!;

    public virtual TourPackage Package { get; set; } = null!;
}
