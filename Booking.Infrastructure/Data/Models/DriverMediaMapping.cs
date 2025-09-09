using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class DriverMediaMapping
{
    public int DriverMediaMappingId { get; set; }

    public int? MediaId { get; set; }

    public int? DriverId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public Guid? ItemGuid { get; set; }

    public virtual Driver? Driver { get; set; }

    public virtual Medium? Media { get; set; }
}
