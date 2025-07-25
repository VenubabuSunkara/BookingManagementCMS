using System;
using System.Collections.Generic;

namespace Entities;

public partial class DriverAndVehicle
{
    public int Id { get; set; }

    public string DriverFirstName { get; set; } = null!;

    public string? DriverLastName { get; set; }

    public string? DriverContact { get; set; }

    public string? DriverEmail { get; set; }

    public string? DriverPhoto { get; set; }

    public string? DriverLicenceNumber { get; set; }

    public string? Description { get; set; }

    public string? VehicleName { get; set; }

    public string? VehicleNumber { get; set; }

    public int? VehicleTypeId { get; set; }

    public string? Features { get; set; }

    public string? AboutOnVehicle { get; set; }

    public int? Capacity { get; set; }

    public bool IsAvailable { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public bool IsApprove { get; set; }

    public string? UsernName { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<DriverVehicleMedium> DriverVehicleMedia { get; set; } = new List<DriverVehicleMedium>();

    public virtual VehicleType? VehicleType { get; set; }
}
