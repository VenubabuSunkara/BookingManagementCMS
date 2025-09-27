using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class PageContent
{
    public int Id { get; set; }

    public string PageName { get; set; } = null!;

    public string PageContent1 { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public string CreateBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public bool IsActive { get; set; }

    public string Placeholder { get; set; } = null!;

    public Guid ItemGuid { get; set; }
}
