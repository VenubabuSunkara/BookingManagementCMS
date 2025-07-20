using System;
using System.Collections.Generic;

namespace Entities;

public partial class PageContent
{
    public int Id { get; set; }

    public string? PageName { get; set; }

    public string? PageContent1 { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreateBy { get; set; }

    public int? UpdatedBy { get; set; }

    public bool? IsActive { get; set; }
}
