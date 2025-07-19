using System;
using System.Collections.Generic;

namespace Entities;

public partial class DriverDetail
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public string? Email { get; set; }

    public string LicenceNumber { get; set; } = null!;

    public string LicenceDocument { get; set; } = null!;

    public string? Description { get; set; }

    public string? DriverImageUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public int Vehicleid { get; set; }

    public virtual Vehicle Vehicle { get; set; } = null!;
}
