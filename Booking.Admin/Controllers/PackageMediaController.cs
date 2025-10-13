using Booking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    public class PackageMediaController(IPackageMediaService mediaService) : BaseController
    {
        private readonly IPackageMediaService _mediaService = mediaService;

        public IActionResult Index()
        {
            return View();
        }
    }
}
