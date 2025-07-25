using System;
using System.Collections.Generic;

namespace Entities;

public partial class EmailTemplate
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? EmailSubject { get; set; }

    public string? EmailBody { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public bool? IsEnabled { get; set; }

    public string? SenderEmail { get; set; }
}
