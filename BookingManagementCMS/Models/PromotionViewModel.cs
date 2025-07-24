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
    [Remote(action: "VerifyCouponCode", controller: "Promotion")]
    public string? Code { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    //[DataType(DataType.Date)]
    public DateOnly? ValidityFrom { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    //[DataType(DataType.Date)]
    public DateOnly? ValidityTo { get; set; }

    public decimal? RangeMin { get; set; }
    public decimal? RangeMax { get; set; }

    public bool IsActive { get; set; }

    public string? MediaUrl { get; set; }
    public int promotionId { get; set; }
}
