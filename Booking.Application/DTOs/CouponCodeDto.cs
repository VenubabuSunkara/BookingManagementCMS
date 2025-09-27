using Booking.Application.CustomValidationAttributes;
using Booking.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Booking.Application.DTOs;

public class CouponCodeDto
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

    public bool? IsActive { get; set; }

    public int? UsageLimit { get; set; }

    public int? UsageCount { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public Guid ItemGuid { get; set; }
}

public class CouponCodeDataTableDto
{
    public int TotalRecords { get; set; }
    public int FilterRecords { get; set; }
    public IEnumerable<CouponCodeDto> CouponCode { get; set; } = [];
    public string NextLink { get; set; } = string.Empty;
    public string PrevLink { get; set; } = string.Empty;
}