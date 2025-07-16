using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.EDMX;

[Table("BookingManagemnet_EventLog")]
public class BookingManagemnetEventLog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("EventID")]
    public int EventId { get; set; }

    [StringLength(5)]
    public required string EventType { get; set; }

    public required DateTime EventTime { get; set; }

    [StringLength(100)]
    public required string Source { get; set; }

    [StringLength(100)]
    public required string EventCode { get; set; }

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
