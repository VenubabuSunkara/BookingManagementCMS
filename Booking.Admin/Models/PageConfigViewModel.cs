using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Models
{
    public class PageConfigViewModel
    {
        public string PageName { get; init; } = null!;
        public string PageContentData { get; init; } = null!;
        public bool IsActive { get; init; }
        public string Placeholder { get; init; } = null!;
        public List<SelectListItem>  PageNames { get; set; } = [];
        public int? Id { get; set; }
    }
}
