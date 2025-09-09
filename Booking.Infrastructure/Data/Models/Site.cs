using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class Site
{
    public int SiteId { get; set; }

    public string SiteName { get; set; } = null!;

    public Guid? TenantId { get; set; }

    public Guid? ItemGuid { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public bool? Isactive { get; set; }
}
