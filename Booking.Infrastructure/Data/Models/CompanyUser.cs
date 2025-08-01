using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class CompanyUser
{
    public int Id { get; set; }

    public Guid TenantId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Contact { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public bool? IsActive { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<CompanyUserRoleMapping> CompanyUserRoleMappings { get; set; } = new List<CompanyUserRoleMapping>();
}
