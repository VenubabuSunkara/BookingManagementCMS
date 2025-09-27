using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class Module
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
