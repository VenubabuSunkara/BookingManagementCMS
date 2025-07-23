using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public partial class Role
{
    [Key]
    public int Id { get; set; }

    [Unicode(false)]
    [Required(ErrorMessage = "Role name is required.")]
    [StringLength(100, ErrorMessage = "Role name cannot exceed 100 characters.")]
    public string Name { get; set; } = null!;
    [StringLength(250, ErrorMessage = "Notes cannot exceed 250 characters.")]
    public string? Notes { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<RolePermissionMapping> RolePermissionMappings { get; set; } = new List<RolePermissionMapping>();
}
