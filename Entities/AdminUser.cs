using System;
using System.Collections.Generic;

namespace Entities;

public partial class AdminUser
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string UserName { get; set; } = null!;

    public string EmailId { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<AdminPermissionMapping> AdminPermissionMappings { get; set; } = new List<AdminPermissionMapping>();
}
