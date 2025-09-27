using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class TourPackageItemAttributeValue
{
    public int PackageId { get; set; }

    public int AttributeId { get; set; }

    public string Value { get; set; } = null!;

    public Guid ItemGuid { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }
}
