using System;
using System.Collections.Generic;

namespace Entities;

public partial class AdminRoleMapping
{
    public int Id { get; set; }

    public int AdminId { get; set; }

    public int RoleId { get; set; }

    public string? Comments { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public bool? IsActive { get; set; }
}
