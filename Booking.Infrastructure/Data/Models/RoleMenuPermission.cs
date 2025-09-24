using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class RoleMenuPermission
{
    public int RoleMenuPermissionId { get; set; }

    public string RoleId { get; set; } = null!;

    public int MenuId { get; set; }

    public int PermissionId { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime UpdatedOn { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid ItemGuid { get; set; }

    public virtual Menu Menu { get; set; } = null!;

    public virtual Permission Permission { get; set; } = null!;

    public virtual AspNetRole Role { get; set; } = null!;
}
