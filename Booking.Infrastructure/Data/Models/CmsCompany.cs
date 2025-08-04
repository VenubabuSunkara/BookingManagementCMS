using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Data.Models;

[Table("CMS_Company")]
public partial class CmsCompany
{
    [Key]
    public int Id { get; set; }

    public Guid TenantId { get; set; }

    [MaxLength(50)]
    public byte[]? Name { get; set; }

    [StringLength(1000)]
    public string? Address { get; set; }

    [StringLength(50)]
    public string? Email { get; set; }

    [StringLength(50)]
    public string? Contact { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    public bool IsActive { get; set; }
}
