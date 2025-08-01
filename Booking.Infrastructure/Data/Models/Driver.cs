using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class Driver
{
    public int Id { get; set; }

    public string FirstName { get; set; }=string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string LicenseNumber { get; set; } = string.Empty;

    public string? Address { get; set; }

    public bool? AvailabilityStatus { get; set; }

    public string? UserName { get; set; }

    public byte[]? PasswordHash { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? TenantId { get; set; }

    public string? AboutOn { get; set; }

    public string? Photo { get; set; }

    public virtual ICollection<DriverVehicleMapping> DriverVehicleMappings { get; set; } = new List<DriverVehicleMapping>();
}
