using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models;

public class PramotionViewModel
{
    [StringLength(200, ErrorMessage = "You have exceeded the maximum allowed characters.")]
    public string? Name { get; set; }

    [StringLength(200, ErrorMessage = "You have exceeded the maximum allowed characters.")]
    public string? Code { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [DataType(DataType.Date)]
    public DateOnly? ValidityFrom { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [DataType(DataType.Date)]
    public DateOnly? ValidityTo { get; set; }

    public decimal? RangeMin { get; set; }
    public decimal? RangeMax { get; set; }

    public bool IsActive { get; set; }

    public string? MediaUrl { get; set; }
}
