using Microsoft.AspNetCore.Mvc;

namespace BookingManagementCMS.Controllers
{
    public class DriverController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
