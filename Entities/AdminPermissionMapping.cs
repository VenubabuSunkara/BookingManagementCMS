using System;
using System.Collections.Generic;

namespace Entities;

public partial class AdminPermissionMapping
{
    public int Id { get; set; }

    public int PermissionId { get; set; }

    public int AdminId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual AdminUser Admin { get; set; } = null!;

    public virtual PermissionGroup Permission { get; set; } = null!;
}
