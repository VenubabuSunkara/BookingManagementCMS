using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Application.Services;
using Booking.Domain.Entities;
using Booking.Web.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Common;
using System.Security.Claims;

namespace Booking.Web.Controllers
{
    [Authorize]
    public class AccountController(IAccountService accountService, IRoleService roleService,
        ILogger<AccountController> logger, SmtpEmailService emailService) : BaseController
    {
        private readonly IAccountService _accountService = accountService;
        private readonly IRoleService _roleService = roleService;
        private readonly ILogger<AccountController> _logger = logger;
        private readonly SmtpEmailService _emailService = emailService;
        [AllowAnonymous]
        public async Task<IActionResult> Login(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View();
            }, token);
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto loginDto)
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

        public async Task<IActionResult> LogOut()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _accountService.LogOut(userId);
            return RedirectToAction("Login");
        }
        [AllowAnonymous]
        public async Task<IActionResult> Register(CancellationToken token)
        {
            RegisterDto registerDto = new();
            var roles = await _roleService.GetAllRoles(token);
            registerDto.Roles = roles.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Name,
            });
            return View(registerDto);
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto model, CancellationToken token)
        {
            var roles = await _roleService.GetAllRoles(token);
            model.Roles = roles.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Name,
            });
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userdto = await _accountService.Register(new UserEntity()
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
            var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = userdto.Id, userdto.RegistrationToken }, Request.Scheme);
            var msg = new IEmailService.EmailMessage(model.Email, "Confirm your email", "", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");
            await _emailService.SendEmailAsync(msg);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Index(CancellationToken token)
        {
            UserDto account = new();
            var roles = await _roleService.GetAllRoles(token);
            account.Roles = [.. roles.Select(x => x.Name)];
            return await Task.Run(() =>
            {
                return View(account);
            }, token);
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token, CancellationToken cancellation)
        {
            var result = await _accountService.ConfirmEmailAsync(userId, token, cancellation);
            return View(result ? "ConfirmEmailSuccess" : "Error");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers([FromBody] DataTableAjaxPostModel request, CancellationToken token,
            string RoleName = "All")
        {
            string search = "";
            if (!String.IsNullOrEmpty(request.search?.value))
                search = request.search?.value ?? string.Empty;
            var users = await _accountService.GetUsers(search, request.length, request.start, RoleName, token);
            return Json(new
            {
                draw = request.draw == 0 ? 1 : request.draw,
                recordsFiltered = users.FilterRecords,
                recordsTotal = users.TotalRecords,
                data = users.UsersDto.ToArray()
            });
        }
        [HttpGet]
        public async Task<IActionResult> PrivacyPolicy(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View();
            }, token);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ForgotPassword(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View();
            }, token);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model, CancellationToken cancellation)
        {
            if (!ModelState.IsValid) return View(model);
            var res = await _accountService.ForgotPassword(model, cancellation);
            if (res == null)
                return RedirectToAction("ForgotPasswordConfirmation");
            var resetLink = Url.Action("ResetPassword", "Account", new { res.Token, email = model.Email }, Request.Scheme);
            await _emailService.SendEmailAsync(new IEmailService.EmailMessage(model.Email, "Reset Password", "", $"Reset your password by <a href='{resetLink}'>clicking here</a>."));
            return RedirectToAction("ForgotPasswordConfirmation");
        }
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string Token, string email, CancellationToken cancellation)
        {
            return await Task.Run(() =>
            {
                ResetPasswordDto dto = new()
                {
                    Token = Token,
                    Email = email
                };

                return View(dto);
            });
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model, CancellationToken cancellation)
        {
            if (!ModelState.IsValid) return View(model);
            ForgotPasswordDto dto = new()
            {
                Email = model.Email,
                Password = model.Password,
                Token = model.Token
            };
            bool isReset = await _accountService.ResetPassword(dto, cancellation);
            if (isReset) return RedirectToAction("ResetPasswordConfirmation");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model, CancellationToken cancellation)
        {
            if (!ModelState.IsValid) return View(model);
            bool isChange = await _accountService.ChangePassword(model, cancellation);
            if (isChange) return RedirectToAction("ChangePasswordConfirmation");
            return View(model);
        }
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        // GET: /Account/ChangePasswordConfirmation
        [HttpGet]
        public IActionResult ChangePasswordConfirmation()
        {
            return View();
        }

        // GET: /Account/ConfirmEmailSuccess
        [HttpGet]
        public IActionResult ConfirmEmailSuccess()
        {
            return View();
        }

        // GET: /Account/Error
        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }

    }
}
