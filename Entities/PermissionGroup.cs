using System;
using System.Collections.Generic;

namespace Entities;

public partial class PermissionGroup
{
    public int Id { get; set; }

    public string GroupName { get; set; } = null!;

    public string? Comment { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<AdminPermissionMapping> AdminPermissionMappings { get; set; } = new List<AdminPermissionMapping>();
}
