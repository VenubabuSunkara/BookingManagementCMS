using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class UserAddress
{
    public int Id { get; set; }

    public int CustomersId { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? CityName { get; set; }

    public string? StateName { get; set; }

    public string? PostalCode { get; set; }

    public string? CountryName { get; set; }

    public string? LandMark { get; set; }

    public string? ContactNo { get; set; }

    public int AddressTypeId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public bool? IsDefault { get; set; }

    public virtual AddressType AddressType { get; set; } = null!;

    public virtual Customer Customers { get; set; } = null!;
}
