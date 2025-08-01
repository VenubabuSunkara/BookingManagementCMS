using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Data.Models;

[Table("DriverVehicleMapping")]
public partial class DriverVehicleMapping
{
    [Key]
    public int Id { get; set; }

    public int? DriverId { get; set; }

    public int? VehicleId { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    [ForeignKey("DriverId")]
    [InverseProperty("DriverVehicleMappings")]
    public virtual Driver? Driver { get; set; }

    [ForeignKey("VehicleId")]
    [InverseProperty("DriverVehicleMappings")]
    public virtual Vehicle? Vehicle { get; set; }
}
