using Booking.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    public class AccountController : BaseController
    {

        public AccountController() { }
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("Login")]
        //// [ValidateAntiForgeryToken]
        //public async Task<ActionResult> Login(LoginDto model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    return View(model);
        //}
    }
}
