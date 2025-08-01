using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Data.Models;

[Index("PhoneNumber", Name = "UQ__Customer__85FB4E382CAFDA36", IsUnique = true)]
[Index("Email", Name = "UQ__Customer__A9D1053474500301", IsUnique = true)]
public partial class Customer
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(15)]
    [Unicode(false)]
    public string PhoneNumber { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string PasswordHash { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string? ReferralCode { get; set; }

    public int? ReferredBy { get; set; }

    public bool? ReferralBonusGranted { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsLocked { get; set; }

    public Guid? TenantId { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<BookingOrder> BookingOrders { get; set; } = new List<BookingOrder>();

    [InverseProperty("Customers")]
    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();

    [InverseProperty("Customer")]
    public virtual ICollection<CustomerRelative> CustomerRelatives { get; set; } = new List<CustomerRelative>();
}
