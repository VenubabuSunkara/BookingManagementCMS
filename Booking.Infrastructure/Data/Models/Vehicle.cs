using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class Vehicle
{
    public int VehicleId { get; set; }

    public string VehicleNumber { get; set; } = null!;

    public string? AboutOnVehicle { get; set; }

    public string Color { get; set; } = null!;

    public string Model { get; set; } = null!;

    public decimal Fare { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public string DefaultImage { get; set; } = null!;

    public Guid ItemGuid { get; set; }

    public string? CarName { get; set; }

    public int? AverageMileage { get; set; }

    public string? InsurnceNumber { get; set; }

    public string? InsurenceValidUntil { get; set; }

    public string? PollucationCertificationNumber { get; set; }

    public string? Fecility { get; set; }

    public int? VehicleTypeId { get; set; }

    public virtual ICollection<BookingOrder> BookingOrders { get; set; } = new List<BookingOrder>();

    public virtual DriverVehicle? DriverVehicle { get; set; }

    public virtual ICollection<DriverVehicleAvailability> DriverVehicleAvailabilities { get; set; } = new List<DriverVehicleAvailability>();

    public virtual ICollection<SeasonalPricing> SeasonalPricings { get; set; } = new List<SeasonalPricing>();

    public virtual ICollection<VehicleFeatureMapping> VehicleFeatureMappings { get; set; } = new List<VehicleFeatureMapping>();

    public virtual ICollection<VehicleMediaMapping> VehicleMediaMappings { get; set; } = new List<VehicleMediaMapping>();
}
