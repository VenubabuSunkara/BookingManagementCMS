using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities;

public class CouponCodeEntity
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public string DiscountType { get; set; } = null!;

    public decimal DiscountValue { get; set; }

    public decimal MinimumAmount { get; set; }

    public decimal MaximumDiscount { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsActive { get; set; }

    public int? UsageLimit { get; set; }

    public int? UsageCount { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public Guid ItemGuid { get; set; }
}

public class CouponCodeDataTableEntity
{
    public int Total { get; set; }
    public int Filtered { get; set; }
    public IEnumerable<CouponCodeEntity> CouponCode { get; set; } = [];
}