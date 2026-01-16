using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class Menu
{
    public int MenuId { get; set; }

    public int? ParentMenuId { get; set; }

    public string MenuName { get; set; } = null!;

    public string MenuUrl { get; set; } = null!;

    public int DisplayOrder { get; set; }

    public string Icon { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedOn { get; set; }

    public Guid ItemGuid { get; set; }

    public virtual ICollection<Menu> InverseParentMenu { get; set; } = new List<Menu>();

    public virtual Menu? ParentMenu { get; set; }

    public virtual ICollection<RoleMenuPermission> RoleMenuPermissions { get; set; } = new List<RoleMenuPermission>();

    public virtual ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();
}
