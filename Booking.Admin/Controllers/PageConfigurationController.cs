using Booking.Application.DTOs.Pages;
using Booking.Application.Interfaces;
using Booking.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class PageConfigurationController(ILogger<PageConfigurationController> logger, IPageConfigurationService pageConfigurationService) : BaseController
    {
        private readonly ILogger<PageConfigurationController> _logger = logger;
        private readonly IPageConfigurationService _pageConfigurationService = pageConfigurationService;
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() =>
            {
                return View();
            });
        }
        public async Task<IActionResult> Create()
        {
            PageConfigViewModel model = new()
            {
                PageNames =
                [
                    new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                    {
                        Text="TurmsAndConditions",
                        Value="TurmsAndConditions"
                    }
                ]
            };
            return await Task.Run(() =>
            {
                return View(model);
            });
        }
        public async Task<IActionResult> Edit(int id, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            if (id == 0)
                return BadRequest("Please select Valid Page Configuration");
            var pageConfig = await _pageConfigurationService.GetByIdAsync(id, token);
            if (pageConfig == null)
                return NotFound("Page Configuration not found");
            var model = new PageConfigViewModel
            {
                Id = pageConfig.Id,
                PageName = pageConfig.PageName,
                PageContentData = pageConfig.PageContentData,
                IsActive = pageConfig.IsActive,
                Placeholder = pageConfig.Placeholder
            };
            return View("Create", model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveConfiguration(PageConfigViewModel model, CancellationToken token)
        {
            if (!ModelState.ValidationState.Equals(Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid))
            {
                return View("Create", model);
            }
            try
            {
                await _pageConfigurationService.AddAsync(new
                   PageConfigurationDto
                (
                    Id: model.Id ?? 0,
                    PageName: model.PageName,
                    PageContentData: model.PageContentData,
                    CreatedBy: base.GetUserName(),
                    UpdatedBy: base.GetUserName(),
                    CreatedOn: DateTime.UtcNow,
                    UpdateOn: DateTime.UtcNow,
                    IsActive: model.IsActive,
                    ItemGuid: Guid.NewGuid(),
                    Placeholder: model.Placeholder
                ), token);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving page configuration");
                return Json("Something went wrong {0}", ex);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoadPageConfiguration([FromBody] DataTableAjaxPostModel request, CancellationToken token)
        {
            if (!ModelState.ValidationState.Equals(Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid))
            {
                return BadRequest("Invalid data.");
            }
            try
            {
                string search = "";
                if (!String.IsNullOrEmpty(request.search?.value))
                    search = request.search?.value ?? string.Empty;
                var result = await _pageConfigurationService.GetAllAsync(request.start, request.length, search, token);
                return Json(new
                {
                    draw = request.draw == 0 ? 1 : request.draw,
                    recordsFiltered = result.FilterRecords,
                    recordsTotal = result.TotalRecords,
                    data = result.PageConfigurationDto
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading page configuration data");
                return Json("Something went wrong {0}", ex);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int PageConfigurationId, CancellationToken token)
        {
            if (!ModelState.ValidationState.Equals(Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid))
            {
                return BadRequest("Invalid data.");
            }
            if (PageConfigurationId == 0)
                return Json("Please select Valid Driver");
            await _pageConfigurationService.DeleteAsync(PageConfigurationId, token);
            return Json(Ok());
        }
    }
}
