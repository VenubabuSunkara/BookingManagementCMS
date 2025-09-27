using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? ReferralCode { get; set; }

    public int? ReferredBy { get; set; }

    public bool? ReferralBonusGranted { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedOn { get; set; }

    public bool IsActive { get; set; }

    public bool? IsLocked { get; set; }

    public Guid TenantId { get; set; }

    public virtual ICollection<BookingOrder> BookingOrders { get; set; } = new List<BookingOrder>();

    public virtual ICollection<CustomerRelative> CustomerRelatives { get; set; } = new List<CustomerRelative>();
}
