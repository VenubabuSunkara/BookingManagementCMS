using System;
using System.Collections.Generic;

namespace Entities;

public partial class Taxis
{
    public int Id { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public double? TaxPercentage { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public bool? IsActive { get; set; }
}
