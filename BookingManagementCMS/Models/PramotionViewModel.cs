using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models;

public class PramotionViewModel
{
    [StringLength(200, ErrorMessage = "You have exceeded the maximum allowed characters.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(200, ErrorMessage = "You have exceeded the maximum allowed characters.")]
    public string? Code { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    //[DataType(DataType.Date)]
    public DateTime? ValidityFrom { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    //[DataType(DataType.Date)]
    public DateTime? ValidityTo { get; set; }

    public decimal? RangeMin { get; set; }
    public decimal? RangeMax { get; set; }

    public bool IsActive { get; set; }

    public string? MediaUrl { get; set; }
}
