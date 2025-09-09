using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class Driver
{
    public int DriverId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string? Email { get; set; }

    public string LicenseNumber { get; set; } = null!;

    public string? Address { get; set; }

    public bool? AvailabilityStatus { get; set; }

    public string UserName { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public string? AboutOn { get; set; }

    public string? Photo { get; set; }

    public bool? ApproveDriver { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? TenantId { get; set; }

    public Guid? ItemGuid { get; set; }

    public virtual ICollection<BookingOrder> BookingOrders { get; set; } = new List<BookingOrder>();

    public virtual ICollection<DriverMediaMapping> DriverMediaMappings { get; set; } = new List<DriverMediaMapping>();

    public virtual ICollection<DriverRating> DriverRatings { get; set; } = new List<DriverRating>();

    public virtual DriverVehicle? DriverVehicle { get; set; }

    public virtual ICollection<DriverVehicleAvailability> DriverVehicleAvailabilities { get; set; } = new List<DriverVehicleAvailability>();
}
