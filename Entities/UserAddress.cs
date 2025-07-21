using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public partial class UserAddress
{
    [Key]
    public int Id { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? AddressLine1 { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? AddressLine2 { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? CityName { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? StateName { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? PostalCode { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? CountryName { get; set; }

    [Column("AddressTypeID")]
    public int AddressTypeId { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDefault { get; set; }

    [ForeignKey("AddressTypeId")]
    [InverseProperty("UserAddresses")]
    public virtual AddressType AddressType { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserAddresses")]
    public virtual User User { get; set; } = null!;
}
