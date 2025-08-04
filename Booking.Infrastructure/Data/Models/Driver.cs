using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Data.Models;

public partial class Driver
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string? FirstName { get; set; }

    [StringLength(100)]
    public string? LastName { get; set; }

    [StringLength(15)]
    public string? PhoneNumber { get; set; }

    [StringLength(50)]
    public string? Email { get; set; }

    [StringLength(50)]
    public string? LicenseNumber { get; set; }

    public string? Address { get; set; }

    public bool? AvailabilityStatus { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? UserName { get; set; }

    [MaxLength(64)]
    public byte[]? PasswordHash { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    public Guid? TenantId { get; set; }

    public string? AboutOn { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Photo { get; set; }

    public bool? ApproveDriver { get; set; }

    [InverseProperty("Driver")]
    public virtual ICollection<DriverVehicleMapping> DriverVehicleMappings { get; set; } = new List<DriverVehicleMapping>();
}
