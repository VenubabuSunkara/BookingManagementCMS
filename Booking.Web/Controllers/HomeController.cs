using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Booking.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => View());

        }
    }
}
