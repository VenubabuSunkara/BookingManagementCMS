using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Application.DTOs
{
    public class PackageGallaryDto
    {
        public int PackageId { get; set; }
        public List<SelectListItem> Packages { get; set; } = [];
        public string PackageMediajson { get; set; } = string.Empty;
    }
}
