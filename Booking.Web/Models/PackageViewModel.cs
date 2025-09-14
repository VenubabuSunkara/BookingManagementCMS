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
        public LocationDto location { get; set; } = new LocationDto();
        public string? LocationApiKey { get; set; }
        public string? LocationDescription { get; set; }


    }
}
