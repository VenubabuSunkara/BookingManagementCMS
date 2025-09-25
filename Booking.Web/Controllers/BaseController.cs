using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Booking.Web.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (User.Identity.IsAuthenticated)
            {
                var userData = User.FindFirst(ClaimTypes.UserData)?.Value;
                ViewData["UserDto"] = userData;
            }
        }
        public int GetUserId()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return 0; // or throw an exception if appropriate
            }
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return 0; // or throw an exception if appropriate
        }
    }
}
