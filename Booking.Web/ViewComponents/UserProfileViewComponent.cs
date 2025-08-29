using Booking.Domain.Entities;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Booking.Application.DTOs;
namespace Booking.Web.ViewComponents
{
    public class UserProfileViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            if (HttpContext.User.Identity?.IsAuthenticated == true)
            {
                var userData = HttpContext.User.FindFirstValue(ClaimTypes.UserData);
                using var stream = new MemoryStream(Encoding.UTF8.GetBytes(userData));
                var userEntity = await JsonSerializer.DeserializeAsync<UserEntity>(stream);
                return View(new UserDto()
                {
                    Username = userEntity.Username,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    Email = userEntity.Email,
                    Contact = userEntity.Contact,
                    Address = userEntity.Address,
                    RoleId = userEntity.RoleId,
                    Roles = userEntity.Roles,
                    Id = userEntity.Id,
                    FullName = userEntity.FullName,
                    ProfilePhoto = userEntity.ProfilePhoto

                });
            }
            return View("Guest");
        }
    }
}
