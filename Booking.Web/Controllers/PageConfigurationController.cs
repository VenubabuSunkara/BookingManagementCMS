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
            return await Task.Run(() =>
            {
                return View();
            });
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
            if(!ModelState.ValidationState.Equals(Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid))
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
