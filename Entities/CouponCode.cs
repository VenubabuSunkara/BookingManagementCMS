using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public partial class CouponCode
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Name { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Code { get; set; }

    public DateOnly? ValidityFrom { get; set; }

    public DateOnly? ValidityTo { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? RangeMin { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? RangeMax { get; set; }

    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public string? MediaUrl { get; set; }
}
