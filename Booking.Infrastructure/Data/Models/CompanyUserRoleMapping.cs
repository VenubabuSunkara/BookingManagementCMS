using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Data.Models;

[Table("CompanyUserRoleMapping")]
public partial class CompanyUserRoleMapping
{
    [Key]
    public int Id { get; set; }

    public int AdminId { get; set; }

    public int RoleId { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Notes { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    [ForeignKey("AdminId")]
    [InverseProperty("CompanyUserRoleMappings")]
    public virtual CompanyUser Admin { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("CompanyUserRoleMappings")]
    public virtual Role Role { get; set; } = null!;
}
