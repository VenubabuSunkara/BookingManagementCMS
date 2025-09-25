using Booking.Application.DTOs.Tour;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Models
{
    public class PackageViewModel
    {
        public int PackagecategoryId { get; set; }
        public List<SelectListItem> PackageCategory { get; set; } = [];
        public TourPackageDto TourPackage { get; set; } = new TourPackageDto();
        public List<PackageMediaDto> PackageMedia { get; set; } = [];
        public TourLocationDto Location { get; set; } = new TourLocationDto();
        public string SingleMediajson { get; set; } = string.Empty;
        public string MultipleMediajson { get; set; } = string.Empty;
        public string? LocationApiKey { get; set; }
    }
}
