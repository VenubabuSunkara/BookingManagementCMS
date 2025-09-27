using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Data.Models;

public partial class PackageMedium
{
    [Key]
    public int Id { get; set; }

    [Column("PackageID")]
    public int PackageId { get; set; }

    [Column("MediaURL")]
    [StringLength(500)]
    public string? MediaUrl { get; set; }

    [StringLength(20)]
    public string? MediaType { get; set; }

    public bool? IsDefault { get; set; }

    [StringLength(500)]
    public string? ThumbnailImage { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("PackageId")]
    [InverseProperty("PackageMedia")]
    public virtual Package Package { get; set; } = null!;
}
