using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Data.Models;

[Table("PageContent")]
public partial class PageContent
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? PageName { get; set; }

    [Column("PageContent")]
    public string? PageContent1 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    public int? CreateBy { get; set; }

    public int? UpdatedBy { get; set; }

    public bool? IsActive { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Placeholder { get; set; }
}
