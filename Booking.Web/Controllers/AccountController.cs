using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Domain.Entities;
using Booking.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace Booking.Web.Controllers
{
    public class AccountController(IAccountService accountService, ILogger<AccountController> logger,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager) : BaseController
    {
        private readonly IAccountService _accountService = accountService;
        private readonly ILogger<AccountController> _logger = logger;
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;

        public async Task<IActionResult> Index()
        {
            return await Task.Run(() =>
            {
                return View();
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            bool isValidUser = await _accountService.Login(loginDto.LoginUser, loginDto.Password);
            if (!isValidUser)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(loginDto);
            }
            return RedirectToAction("Index", "Home");
            //var user = await _userManager.FindByNameAsync(loginDto.LoginUser);
            //if (user == null)
            //{
            //    ModelState.AddModelError("", "Invalid login attempt.");
            //    return View(loginDto);
            //}
            //var result = await _signInManager.PasswordSignInAsync(
            //    user, loginDto.Password, loginDto.RememberMe, lockoutOnFailure: false);

            //if (result.Succeeded)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
            //    ModelState.AddModelError("", "Invalid login attempt.");
            //    return View(loginDto);
            //}
        }
        public async Task<IActionResult> Register()
        {
            return await Task.Run(() =>
            {
                return View();
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _accountService.Register(new UserEntity()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Contact = model.Contact,
                Password = model.Password,
                Username = model.Email,
                Address = string.Empty,
                TenantId = Guid.NewGuid(),
                IsActive = true               
            });
            // Process registration (e.g., create account)

            return RedirectToAction("Index");
        }
    }
}
