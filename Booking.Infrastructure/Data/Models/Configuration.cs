using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class Configuration
{
    public int Id { get; set; }

    public string KeyName { get; set; } = null!;

    public string KeyValue { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public Guid ItemGuid { get; set; }
}
