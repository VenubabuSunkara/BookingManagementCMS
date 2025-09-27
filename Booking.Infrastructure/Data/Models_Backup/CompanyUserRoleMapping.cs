using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class CompanyUserRoleMapping
{
    public int Id { get; set; }

    public int AdminId { get; set; }

    public int RoleId { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual CompanyUser Admin { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
