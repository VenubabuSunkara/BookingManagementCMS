using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace Booking.Web.Controllers
{
    public class MediaController(ILogger<MediaController> logger) : BaseController
    {
        private readonly ILogger<MediaController> _logger = logger;
        public async Task<IActionResult> Index(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View();
            }, token);
        }
    }
}
