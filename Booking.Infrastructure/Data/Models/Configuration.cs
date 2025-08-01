using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Data.Models;

public partial class Configuration
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string KeyName { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string KeyValue { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }
}
