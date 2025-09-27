using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class TourPackageItem
{
    public int PackageItemId { get; set; }

    public int PackageId { get; set; }

    public int ItemTypeId { get; set; }

    public string? ItemName { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? SequenceNo { get; set; }

    public decimal? Cost { get; set; }

    public int? ItemOrder { get; set; }

    public Guid ItemGuid { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual TourItemType ItemType { get; set; } = null!;

    public virtual TourPackage Package { get; set; } = null!;

    public virtual ICollection<TourPackageItemAttributeValue> TourPackageItemAttributeValues { get; set; } = new List<TourPackageItemAttributeValue>();
}
