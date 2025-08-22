using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class TourItemAttribute
{
    public int AttributeId { get; set; }

    public int ItemTypeId { get; set; }

    public string AttributeName { get; set; } = null!;

    public int? ItemOrder { get; set; }

    public Guid ItemGuid { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual TourItemType ItemType { get; set; } = null!;

    public virtual ICollection<TourPackageItemAttributeValue> TourPackageItemAttributeValues { get; set; } = new List<TourPackageItemAttributeValue>();
}
