using System;
using System.Collections.Generic;

namespace Entities;

public partial class Configuration
{
    public int Id { get; set; }

    public string KeyName { get; set; } = null!;

    public string KeyValue { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }
}
