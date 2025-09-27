using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class CompanyUser
{
    public int Id { get; set; }

    public Guid TenantId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public bool? IsActive { get; set; }

    public string? Address { get; set; }

    public string? UserId { get; set; }

    public virtual AspNetUser? User { get; set; }
}
