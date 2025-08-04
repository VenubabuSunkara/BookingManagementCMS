using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Data.Models;

public partial class VehicleMedium
{
    [Key]
    public int Id { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string MediaName { get; set; } = null!;

    public string MediaUrl { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string MediaType { get; set; } = null!;

    public int VehicleId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsDefault { get; set; }

    public string? ThumbnailUrl { get; set; }

    [ForeignKey("VehicleId")]
    [InverseProperty("VehicleMedia")]
    public virtual Vehicle Vehicle { get; set; } = null!;
}
