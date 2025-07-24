using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public partial class DriverAndVehicle
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string DriverFirstName { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string? DriverLastName { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? DriverContact { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? DriverEmail { get; set; }

    public string? DriverPhoto { get; set; }

    [StringLength(50)]
    public string? DriverLicenceNumber { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? VehicleName { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? VehicleNumber { get; set; }

    public int? VehicleTypeId { get; set; }

    public string? Features { get; set; }

    public string? AboutOnVehicle { get; set; }

    public int? Capacity { get; set; }

    public bool IsAvailable { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    public bool isApprove { get; set; }

    public string Password { get; set; }
    public string UsernName { get; set; }

    [InverseProperty("DriverVehicle")]
    public virtual ICollection<DriverVehicleMedium> DriverVehicleMedia { get; set; } = new List<DriverVehicleMedium>();

    [ForeignKey("VehicleTypeId")]
    [InverseProperty("DriverAndVehicles")]
    public virtual VehicleType? VehicleType { get; set; }
}
