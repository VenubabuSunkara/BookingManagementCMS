using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class RoleMenu
{
    public int RoleMenuId { get; set; }

    public string RoleId { get; set; } = null!;

    public int MenuId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedOn { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid ItemGuid { get; set; }

    public virtual Menu Menu { get; set; } = null!;

    public virtual AspNetRole Role { get; set; } = null!;
}
