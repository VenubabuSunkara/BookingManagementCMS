using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public partial class VehicleType
{
    [Key]
    public int Id { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("VehicleType")]
    [StringLength(100)]
    public string VehicleType1 { get; set; } = null!;

    public bool? IsActive { get; set; }

    [InverseProperty("VehicleType")]
    public virtual ICollection<DriverAndVehicle> DriverAndVehicles { get; set; } = new List<DriverAndVehicle>();
}
