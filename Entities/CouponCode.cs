using System;
using System.Collections.Generic;

namespace Entities;

public partial class CouponCode
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public DateOnly? ValidityFrom { get; set; }

    public DateOnly? ValidityTo { get; set; }

    public decimal? RangeMin { get; set; }

    public decimal? RangeMax { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public string? MediaUrl { get; set; }
}
