using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class Taxis
{
    public int Id { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public int? TaxPercentage { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }
}
