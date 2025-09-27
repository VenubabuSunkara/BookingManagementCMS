using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? TwoLetterIsoCode { get; set; }

    public string? ThreeLetterIsoCode { get; set; }

    public bool AllowsBilling { get; set; }

    public bool AllowsShipping { get; set; }

    public int NumericIsoCode { get; set; }

    public bool SubjectToVat { get; set; }

    public bool Published { get; set; }

    public int DisplayOrder { get; set; }

    public bool LimitedToStores { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public int? CreatedBy { get; set; }
}
