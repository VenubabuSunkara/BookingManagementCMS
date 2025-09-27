using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class VehicleFeature
{
    public int FeatureId { get; set; }

    public string FeatureName { get; set; } = null!;

    public string FeatureType { get; set; } = null!;

    public string FeatureValue { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? ItemGuid { get; set; }

    public virtual ICollection<VehicleFeatureMapping> VehicleFeatureMappings { get; set; } = new List<VehicleFeatureMapping>();
}
