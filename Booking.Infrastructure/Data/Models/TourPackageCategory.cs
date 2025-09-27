using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class TourPackageCategory
{
    public int Id { get; set; }

    public Guid ItemGuid { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime UpdatedOn { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual ICollection<TourPackage> TourPackages { get; set; } = new List<TourPackage>();
}
