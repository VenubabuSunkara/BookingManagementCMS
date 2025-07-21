using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities;

[Table("RolePermissionMapping")]
public partial class RolePermissionMapping
{
    [Key]
    public int Id { get; set; }

    public int PermissionId { get; set; }

    public int RoleId { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("PermissionId")]
    [InverseProperty("RolePermissionMappings")]
    public virtual PermissionGroup Permission { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("RolePermissionMappings")]
    public virtual Role Role { get; set; } = null!;
}
