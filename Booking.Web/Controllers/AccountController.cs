using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Controllers
{
    public class AccountController(IAccountService accountService, IRoleService roleService,
        ILogger<AccountController> logger) : BaseController
    {
        private readonly IAccountService _accountService = accountService;
        private readonly IRoleService _roleService = roleService;
        private readonly ILogger<AccountController> _logger = logger;

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
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(loginDto);
            }
            var UserData = await _accountService.Login(new LoginEntity()
            {
                Email = loginDto.LoginUser,
                Password = loginDto.Password,
                RememberMe = loginDto.RememberMe
            });
            if (UserData == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(loginDto);
            }
            // base.UserDto=UserData;
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Register()
        {
            RegisterDto registerDto = new();
            var roles = await _roleService.GetAllRoles();
            registerDto.Roles = roles.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Name,
            });
            return View(registerDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var roles = await _roleService.GetAllRoles();
            model.Roles = roles.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Name,
            });
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
                IsActive = true,
                RoleId = model.SelectedRoleId,
            });
            return RedirectToAction("Index");
        }
    }
}
