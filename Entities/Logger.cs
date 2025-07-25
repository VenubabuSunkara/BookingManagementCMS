using System;
using System.Collections.Generic;

namespace Entities;

public partial class Logger
{
    public int EventId { get; set; }

    public string EventType { get; set; } = null!;

    public DateTime EventTime { get; set; }

    public string Source { get; set; } = null!;

    public string EventCode { get; set; } = null!;

    public string? UserName { get; set; }

    public string? Ipaddress { get; set; }

    public string? EventDescription { get; set; }

    public string? EventUrl { get; set; }

    public string? EventMachineName { get; set; }
}
