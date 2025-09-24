using Booking.Application.DTOs.Tour;
using Booking.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Models
{
    public class PackageViewModel
    {
        public int PackagecategoryId { get; set; }
        public List<SelectListItem> PackageCategory { get; set; } = new List<SelectListItem>();
        public TourPackageDto TourPackage { get; set; } = new TourPackageDto();
        public PackageMediaDto packageMedia { get; set; } = new PackageMediaDto();
        public TourLocationDto location { get; set; } = new TourLocationDto();
        public string? LocationApiKey { get; set; }
    }
}
