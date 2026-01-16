using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class AddressType
{
    public int Id { get; set; }

    public string TypeName { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedOn { get; set; }

    public Guid ItemGuid { get; set; }

    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();
}
