using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class EmailTemplate
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string EmailSubject { get; set; } = null!;

    public string EmailBody { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public bool IsEnabled { get; set; }

    public string? SenderEmail { get; set; }

    public Guid ItemGuid { get; set; }
}
