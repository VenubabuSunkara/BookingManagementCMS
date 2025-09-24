using Booking.Application.DTOs;
using Booking.Application.DTOs.Tour;
using Booking.Application.Interfaces;
using Booking.Application.Services;
using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using NuGet.Common;

namespace Booking.Web.Controllers
{
    public class PackagesController : BaseController
    {
        private readonly ILogger<PackagesController> _logger;
        private readonly IPackageService _packageService;
        private readonly IPackageCategoryService _packageCategoryService;
        public PackagesController(ILogger<PackagesController> logger, IPackageService packageService, IPackageCategoryService packageCategoryService)
        {
            _logger = logger;
            _packageService = packageService;
            _packageCategoryService = packageCategoryService;
        }
        public async Task<IActionResult> Index(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View("Index");
            }, token);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetAllPackages([FromBody] DataTableAjaxPostModel request,
        CancellationToken cancellationToken)
        {
            var result = await _packageService.GetPackages(request.start, request.length, "", 0);
            return Json(new
            {
                draw = request.draw == 0 ? 1 : request.draw,
                recordsFiltered = result.FilterRecords,
                recordsTotal = result.TotalRecords,
                data = result.PackagesData.Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.DurationDays,
                    x.Destination,
                    x.ShortDescription,
                    x.Source,
                    x.BannerImage,
                    x.Price,
                }).ToArray()
            });
        }

        public async Task<IActionResult> AddCategory(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View();
            }, token);
        }

        public async Task<IActionResult> ViewPackage(int PackageId, CancellationToken token)
        {
            if (PackageId > 0)
            {

            }
            return await Task.Run(() => { return View(); });
        }
        public async Task<IActionResult> AddPackage(CancellationToken token)
        {
            var tourPackages = await _packageCategoryService.GetTourPackageCategory(token);
            PackageViewModel model = new()
            {
                PackageCategory = [.. tourPackages.Select(x => new SelectListItem()
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString()
                })]
            };

            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPackage(int Id)
        {
            return await Task.Run(() => { return View(); });
        }
        [HttpPost]
        public async Task<IActionResult> Single(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var path = Path.Combine("wwwroot/uploads", file.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
            }
            return Ok(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Multiple(List<IFormFile> files)
        {
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var path = Path.Combine("wwwroot/uploads", file.FileName);
                    using var stream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(stream);
                }
            }
            return Ok(new { success = true });
        }
    }
}
