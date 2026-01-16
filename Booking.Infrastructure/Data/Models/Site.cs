using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class Site
{
    public int SiteId { get; set; }

    public string SiteName { get; set; } = null!;

    public Guid TenantId { get; set; }

    public Guid ItemGuid { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedOn { get; set; }

    public bool Isactive { get; set; }
}
