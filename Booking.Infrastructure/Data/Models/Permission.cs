using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class Permission
{
    public int Id { get; set; }

    public int ModuleId { get; set; }

    public string Action { get; set; } = null!;

    public string? Description { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual Module Module { get; set; } = null!;

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
