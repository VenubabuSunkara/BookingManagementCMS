using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Data.Models;

[Table("Vehicle")]
public partial class Vehicle
{
    [Key]
    public int Id { get; set; }

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

    public int? SeatingCapacity { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? Color { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Make { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Model { get; set; }

    [InverseProperty("Vehicle")]
    public virtual ICollection<DriverVehicleMapping> DriverVehicleMappings { get; set; } = new List<DriverVehicleMapping>();

    [InverseProperty("Vehicle")]
    public virtual ICollection<VehicleMedium> VehicleMedia { get; set; } = new List<VehicleMedium>();
}
