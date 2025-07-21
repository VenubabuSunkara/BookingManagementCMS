using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities;

[Table("AdminUser")]
public partial class AdminUser
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string? LastName { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string UserName { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string EmailId { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string PasswordHash { get; set; } = null!;

    public int? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }
}
