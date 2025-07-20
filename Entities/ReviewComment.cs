using System;
using System.Collections.Generic;

namespace Entities;

public partial class ReviewComment
{
    public int Id { get; set; }

    public string? ReviewComment1 { get; set; }

    public decimal? Rating { get; set; }

    public int? DriverVechileId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }
}
