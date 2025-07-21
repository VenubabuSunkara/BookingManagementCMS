using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public partial class EmailTemplate
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Name { get; set; }

    [StringLength(200)]
    public string? EmailSubject { get; set; }

    public string? EmailBody { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public bool? IsEnabled { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SenderEmail { get; set; }
}
