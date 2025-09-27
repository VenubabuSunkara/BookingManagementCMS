using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class CustomerAddress
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string AddressLine1 { get; set; } = null!;

    public string? AddressLine2 { get; set; }

    public string CityName { get; set; } = null!;

    public string StateName { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public string CountryName { get; set; } = null!;

    public string? LandMark { get; set; }

    public string ContactNo { get; set; } = null!;

    public int AddressTypeId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedOn { get; set; }

    public bool? IsDefault { get; set; }

    public Guid ItemGuid { get; set; }

    public virtual AddressType AddressType { get; set; } = null!;
}
