using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public partial class DriverVehicleMedium
{
    [Key]
    public int Id { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string MediaName { get; set; } = null!;

    public string MediaValue { get; set; } = null!;

    public int DriverVehicleId { get; set; }

    public bool IsActive { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Title { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    [ForeignKey("DriverVehicleId")]
    [InverseProperty("DriverVehicleMedia")]
    public virtual DriverAndVehicle DriverVehicle { get; set; } = null!;
}
