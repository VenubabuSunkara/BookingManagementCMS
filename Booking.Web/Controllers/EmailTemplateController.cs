using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Application.Services;
using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Web.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Booking.Web.Controllers
{
    public class EmailTemplateController(ILogger<EmailTemplateController> logger,
        IEmailTemplateService emailTemplateService, ISmtpEmailService smtpEmailService) : BaseController
    {
        private readonly ILogger<EmailTemplateController> _logger = logger;
        private readonly ISmtpEmailService _smtpEmailService = smtpEmailService;
        private readonly IEmailTemplateService _emailTemplateService = emailTemplateService;

        public async Task<IActionResult> Index(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View();
            }, token);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoadEmailTemplateData([FromBody] DataTableAjaxPostModel request, CancellationToken token)
        {
            try
            {
                string search = "";
                if (!String.IsNullOrEmpty(request.search?.value))
                    search = request.search?.value ?? string.Empty;
                var result = await _emailTemplateService.GetEmailTemplates(search, request.length, request.start, token);
                return Json(new
                {
                    draw = request.draw == 0 ? 1 : request.draw,
                    recordsFiltered = result.FilterRecords,
                    recordsTotal = result.TotalRecords,
                    data = result.EmailTemplatesDto
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: $"{LoadEmailTemplateData}");
                return Json("Something went wrong {0}", ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendTestMail([FromBody] EmailTemplateDto model, CancellationToken token)
        {
            try
            {
                await _smtpEmailService.SendEmailAsync(new ISmtpEmailService.EmailMessage(model.SenderEmail, model.EmailSubject, "", model.EmailBody));
                return Json(new { success = true, message = "Mail sent successfully!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Json(new { success = false, message = "Some thing went wrong..." });
            }
        }

        public async Task<IActionResult> Create(CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return View("Index");
                }, token);
            return await Task.Run(() =>
            {
                RoleDto dto = new()
                {
                    isEdit = false
                };
                return View("Create", dto);
            }, token);
        }
        /// <summary>
        /// Fetching Data for edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(string id, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return View("Index");
                }, token);
            var role = await _roleService.GetByIdAsync(id, token);
            if (role == null) return NotFound();
            role.isEdit = true;
            return View("Create", role);
        }
        /// <summary>
        /// AJAX call for role name uniqueness check
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> IsRoleNameAvailable(string name, CancellationToken token, int id = 0)
        {
            var exists = await _roleService.ExistsByNameAsync(name, token, id);
            return Json(!exists);
        }
        /// <summary>
        /// Create or Update Role
        /// </summary>
        /// <param name="rolePayload"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(RoleDto role, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return View("Index");
                }, token);
            if (!ModelState.IsValid)
                return View("Create", role);
            //bool exists = await _roleService.ExistsByNameAsync(role.Name, token);
            if (role.isEdit == true)
            {
                await _roleService.UpdateAsync(role, token);
            }
            else
            {
                await _roleService.CreateAsync(role, token);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Deleting Role by RoleId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return View("Index");
                }, token);
            await _roleService.DeleteAsync(id, token);
            TempData["SuccessMessage"] = "Role deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
