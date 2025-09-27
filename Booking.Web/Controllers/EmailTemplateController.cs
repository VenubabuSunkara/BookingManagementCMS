using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc;

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
                return View("Create");
            }, token);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int TemplateId, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return View("Index");
                }, token);
            var emailTemplate = await _emailTemplateService.GetEmailTemplateByIdAsync(TemplateId, token);
            if (emailTemplate == null)
            {
                return await Task.Run(() =>
                {
                    return View("Index");
                }, token);
            }
            return View("Create", emailTemplate);
        }

        [HttpGet]
        public async Task<JsonResult> IsEmailTemplateAvailable(string TemplateName, CancellationToken token)
        {
            var exists = await _emailTemplateService.ExistsByNameAsync(TemplateName, token);
            return Json(!exists);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(EmailTemplateDto emailtemplate, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return View("Index");
                }, token);
            if (!ModelState.IsValid)
                return View("Create", emailtemplate);
            if (emailtemplate.Id == 0)
            {
                await _emailTemplateService.UpdateAsync(emailtemplate, token);
            }
            else
            {
                await _emailTemplateService.CreateAsync(emailtemplate, token);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int TemplateId, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return View("Index");
                }, token);
            await _emailTemplateService.DeleteAsync(TemplateId, token);
            TempData["SuccessMessage"] = "Template deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
