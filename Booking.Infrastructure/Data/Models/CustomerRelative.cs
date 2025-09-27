using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class CustomerRelative
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string FullName { get; set; } = null!;

    public int Age { get; set; }

    public string Gender { get; set; } = null!;

    public string? Relationship { get; set; }

    public string? PhoneNumber { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedOn { get; set; }

    public Guid ItemGuid { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
