using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class Driver
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? LicenseNumber { get; set; }

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

    public bool? ApproveDriver { get; set; }

    public virtual ICollection<BookingOrder> BookingOrders { get; set; } = new List<BookingOrder>();

    public virtual ICollection<DriverVehicleMapping> DriverVehicleMappings { get; set; } = new List<DriverVehicleMapping>();
}
