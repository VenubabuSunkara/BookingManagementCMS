using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Application.Services;
using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Booking.Web.Controllers
{
    public class PackagesController : BaseController
    {
        private readonly ILogger<PackagesController> _logger;
        private readonly IPackageService _packageService;
        public PackagesController(ILogger<PackagesController> logger, IPackageService packageService)
        {
            _logger = logger;
            _packageService = packageService;
        }
        public async Task<IActionResult> Index(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View("Index");
            }, token);
        }
        public async Task<IActionResult> PackageCategory(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View("PackageCategory");
            }, token);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetAllPackageCategories([FromBody] DataTableAjaxPostModel request)
        {
            var tourPackages = await _packageService.GetTourPackageCategory();
            return Json(new
            {
                draw = request.draw == 0 ? 1 : request.draw,
                recordsFiltered = tourPackages.Count(),
                recordsTotal = tourPackages.Count(),
                data = tourPackages.Select(x => new
                {
                    Id = x.Id,
                    x.CategoryName,
                    x.NoOfPackages
                }).ToArray()
            });

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchPackages([FromBody] DataTableAjaxPostModel request, CancellationToken cancellationToken)
        {
            return null;
            //var result ; //await _packageService.SearchPackages(request.start, request.length);
            //return Json(new
            //{
            //    draw = request.draw == 0 ? 1 : request.draw,
            //    recordsFiltered = result.FilterRecords,
            //    recordsTotal = result.TotalRecords,
            //    data = result.PackagesData.Select(x => new
            //    {
            //        Id = x.Id,
            //        Title = x.Title,
            //        x.DurationDays,
            //        x.Destination,
            //        x.Source,
            //        PackageImage = x.PackageMedia?.ThumbnailImage,
            //        x.Price
            //    }).ToArray()
            //});
        }

        public async Task<IActionResult> ViewPackage(int PackageId, CancellationToken token)
        {
            if (PackageId > 0)
            {

            }
            return await Task.Run(() => { return View(); });
        }
        public async Task<IActionResult> AddPackage()
        {
            return await Task.Run(() => { return View(); });
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPackage(int Id)
        {
            return await Task.Run(() => { return View(); });
        }
    }
}
