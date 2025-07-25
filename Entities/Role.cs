using System;
using System.Collections.Generic;

namespace Entities;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Notes { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<RolePermissionMapping> RolePermissionMappings { get; set; } = new List<RolePermissionMapping>();
}
