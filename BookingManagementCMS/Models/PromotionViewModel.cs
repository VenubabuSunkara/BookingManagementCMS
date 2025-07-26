using CMS.CustomValidationAttributes;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models;

public class PromotionViewModel
{
    [StringLength(200, ErrorMessage = "You have exceeded the maximum allowed characters.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(200, ErrorMessage = "You have exceeded the maximum allowed characters.")]
    [Remote(action: "VerifyCouponCode", controller: "Promotion",AdditionalFields = nameof(promotionId))]
    public string? Code { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    public DateTime? ValidityFrom { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [DateComparisonAttribute("ValidityFrom", ErrorMessage = "End date must be greater than the start date.")]
    public DateTime? ValidityTo { get; set; }

    public decimal? RangeMin { get; set; }
    public decimal? RangeMax { get; set; }

    public bool IsActive { get; set; }

    public IFormFile? FileUpload { get; set; }
    public string? MediaUrl { get; set; }
    public int promotionId { get; set; }
}
