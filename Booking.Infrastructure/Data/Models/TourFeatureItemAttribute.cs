using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class TourFeatureItemAttribute
{
    public int AttributeId { get; set; }

    public int ItemTypeId { get; set; }

    public string AttributeName { get; set; } = null!;

    public int? ItemOrder { get; set; }

    public Guid ItemGuid { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public virtual TourFeatureItemType ItemType { get; set; } = null!;
}
