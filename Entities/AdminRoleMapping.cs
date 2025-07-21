using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities;

[Table("AdminRoleMapping")]
public partial class AdminRoleMapping
{
    [Key]
    public int Id { get; set; }

    public int AdminId { get; set; }

    public int RoleId { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Comments { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public bool? IsActive { get; set; }
}
