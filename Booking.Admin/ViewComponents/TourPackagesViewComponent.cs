using Booking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Booking.Web.ViewComponents
{
    public class TourPackagesViewComponent : ViewComponent
    {
        private readonly ILogger<TourPackagesViewComponent> _logger;
        private readonly IPackageService _packageService;
        public TourPackagesViewComponent(ILogger<TourPackagesViewComponent> logger, IPackageService packageService)
        {
            _logger = logger;
            _packageService = packageService;
        }
        public async Task<IViewComponentResult> InvokeAsync(CancellationToken token)
        {

            if (HttpContext.User.Identity?.IsAuthenticated == true)
            {
                var packages = await _packageService.GetPackages(0, 10, "", 0, token);
                return View(packages.PackagesData);
            }
            return View("Unauthorized");
        }
    }
}
