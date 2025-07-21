using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public partial class ReviewComment
{
    [Key]
    public int Id { get; set; }

    [Column("ReviewComment")]
    public string? ReviewComment1 { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? Rating { get; set; }

    public int? DriverVechileId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }
}
