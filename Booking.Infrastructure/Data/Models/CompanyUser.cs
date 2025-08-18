using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Data.Models;

[Table("CompanyUser")]
public partial class CompanyUser
{
    [Key]
    public int Id { get; set; }

    public Guid TenantId { get; set; }

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
    public string Email { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Contact { get; set; } = null!;

    public int? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    public bool? IsActive { get; set; }

    public string? Address { get; set; }

    public byte[]? Passwordhash { get; set; }

    [InverseProperty("Admin")]
    public virtual ICollection<CompanyUserRoleMapping> CompanyUserRoleMappings { get; set; } = new List<CompanyUserRoleMapping>();
}
