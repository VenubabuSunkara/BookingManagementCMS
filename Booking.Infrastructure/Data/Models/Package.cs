using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Data.Models;

public partial class Package
{
    [Key]
    public int Id { get; set; }

    [StringLength(150)]
    public string? Title { get; set; }

    [StringLength(1000)]
    public string? ShortDescription { get; set; }

    public string? FullDescription { get; set; }

    [StringLength(100)]
    public string? Source { get; set; }

    [StringLength(100)]
    public string? Destination { get; set; }

    public int? DurationDays { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Price { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    public string? TurmsandConditions { get; set; }

    public bool? IsActive { get; set; }

    [InverseProperty("Package")]
    public virtual ICollection<BookingOrder> BookingOrders { get; set; } = new List<BookingOrder>();

    [InverseProperty("Package")]
    public virtual ICollection<PackageMedium> PackageMedia { get; set; } = new List<PackageMedium>();
}
