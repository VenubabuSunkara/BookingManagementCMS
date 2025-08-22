using System;
using System.Collections.Generic;

namespace Entities;

public partial class UserAddress
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? CityName { get; set; }

    public string? StateName { get; set; }

    public string? PostalCode { get; set; }

    public string? CountryName { get; set; }

    public int AddressTypeId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDefault { get; set; }

    public virtual AddressType AddressType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
