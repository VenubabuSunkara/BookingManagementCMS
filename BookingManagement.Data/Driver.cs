using Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Driver
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DriverID { get; set; }

    [Required]
    [MaxLength(100)]
    public required string FullName { get; set; }

    [Required]
    [MaxLength(15)]
    public required string PhoneNumber { get; set; }

    [Required]
    [MaxLength(50)]
    public required string LicenseNumber { get; set; }

    [MaxLength(4000)]
    public string? Address { get; set; }

    [MaxLength(4000)]
    public string? Description { get; set; }

    // Foreign Key
    [ForeignKey("VehicleType")]
    public int VehicleTypeId { get; set; }

    public VehicleType? VehicleType { get; set; }  // Navigation Property

    public bool AvailabilityStatus { get; set; } = true;

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int? UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
