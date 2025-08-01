using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class AddressType
{
    public int Id { get; set; }

    public string TypeName { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();
}
