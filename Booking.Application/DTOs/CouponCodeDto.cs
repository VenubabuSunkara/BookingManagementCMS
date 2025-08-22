using Booking.Application.CustomValidationAttributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Booking.Application.DTOs;

public class CouponCodeDto
{
    public int CouponCodeId { get; set; }
    [Required(ErrorMessage = "This field is required.")]
    [StringLength(200, ErrorMessage = "You have exceeded the maximum allowed characters.")]
    [Remote(action: "VerifyCouponCode", controller: "CouponCode", AdditionalFields = nameof(CouponCodeId))]
    public string? Code { get; set; }
    [Required(ErrorMessage = "This field is required.")]
    public DateOnly? ValidityFrom { get; set; }
    [Required(ErrorMessage = "This field is required.")]
    [DateComparisonAttribute("ValidityFrom", ErrorMessage = "End date must be greater than the start date.")]
    public DateOnly? ValidityTo { get; set; }
    public decimal? PriceRangeMin { get; set; }
    public decimal? PriceRangeMax { get; set; }
    public string? DiscountType { get; set; }
    public string? DiscountValue { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public string? MediaUrl { get; set; }
    public IFormFile? FileUpload { get; set; }
}
