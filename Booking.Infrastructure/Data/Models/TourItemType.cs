using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class TourItemType
{
    public int ItemTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public int? ItemOrder { get; set; }

    public Guid ItemGuid { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<TourItemAttribute> TourItemAttributes { get; set; } = new List<TourItemAttribute>();

    public virtual ICollection<TourPackageItem> TourPackageItems { get; set; } = new List<TourPackageItem>();
}
