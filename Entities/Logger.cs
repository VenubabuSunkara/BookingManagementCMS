using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities;

[Table("Logger")]
public partial class Logger
{
    [Key]
    [Column("EventID")]
    public int EventId { get; set; }

    [StringLength(5)]
    public string EventType { get; set; } = null!;

    public DateTime EventTime { get; set; }

    [StringLength(100)]
    public string Source { get; set; } = null!;

    [StringLength(100)]
    public string EventCode { get; set; } = null!;

    [StringLength(250)]
    public string? UserName { get; set; }

    [Column("IPAddress")]
    [StringLength(100)]
    public string? Ipaddress { get; set; }

    public string? EventDescription { get; set; }

    [StringLength(2000)]
    public string? EventUrl { get; set; }

    [StringLength(100)]
    public string? EventMachineName { get; set; }
}
