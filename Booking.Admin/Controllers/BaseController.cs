using Booking.Domain.Entities;
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

            if (User?.Identity?.IsAuthenticated == true)
            {
                var userData = User.FindFirst(ClaimTypes.UserData)?.Value;
                ViewData["UserDto"] = userData;
            }
        }
        public string GetUserId()
        {
            if (User?.Identity?.IsAuthenticated == false)
            {
                return "System"; // or throw an exception if appropriate
            }
            var userData = User?.FindFirst(ClaimTypes.UserData)?.Value;
            if (string.IsNullOrWhiteSpace(userData)) return "System";
            var userDto = System.Text.Json.JsonSerializer.Deserialize<UserEntity>(userData);
            if (userData is null) return "System";
            return $"{userDto?.Id}";
        }
        public string GetUserName()
        {
            if (User?.Identity?.IsAuthenticated == false)
            {
                return "System"; // or throw an exception if appropriate
            }
            var userData = User?.FindFirst(ClaimTypes.UserData)?.Value;
            if (string.IsNullOrWhiteSpace(userData)) return "System";
            var userDto = System.Text.Json.JsonSerializer.Deserialize<UserEntity>(userData);
            if (userData is null) return "System";
            return $"{userDto?.FirstName} {userDto?.LastName}";
        }
    }
}
