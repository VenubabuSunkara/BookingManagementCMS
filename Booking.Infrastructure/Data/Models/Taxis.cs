using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Data.Models;

public partial class Taxis
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Country { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? City { get; set; }

    public int? TaxPercentage { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }
}
