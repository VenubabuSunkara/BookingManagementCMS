using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class CmsCompany
{
    public int Id { get; set; }

    public Guid TenantId { get; set; }

    public byte[]? Name { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public string? Contact { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public bool IsActive { get; set; }
}
