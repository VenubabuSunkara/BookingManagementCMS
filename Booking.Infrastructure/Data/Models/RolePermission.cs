using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Data.Models;

public partial class RolePermission
{
    [Key]
    public int Id { get; set; }

    [Column("RoleID")]
    public int RoleId { get; set; }

    [Column("PermissionID")]
    public int PermissionId { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    [ForeignKey("PermissionId")]
    [InverseProperty("RolePermissions")]
    public virtual Permission Permission { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("RolePermissions")]
    public virtual Role Role { get; set; } = null!;
}
