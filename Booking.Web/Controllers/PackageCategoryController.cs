using Booking.Application.DTOs;
using Booking.Application.DTOs.Tour;
using Booking.Application.Enums;
using Booking.Application.Interfaces;
using Booking.Application.Services;
using Booking.Web.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    public class PackageCategoryController(ILogger<PackageCategoryController> logger,

        IPackageCategoryService packageCategoryService, FileReaderService readerService) : BaseController
    {
        private readonly ILogger<PackageCategoryController> _logger = logger;
        private readonly IPackageCategoryService _packageCategoryService = packageCategoryService;
        private readonly FileReaderService _readerService = readerService;
        public async Task<IActionResult> Index(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View();
            }, token);
        }
        public async Task<IActionResult> AddCategory(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View("AddCategory");
            }, token);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPackageCategory(int CategoryId, CancellationToken token)
        {
            var CategoryModel = await _packageCategoryService.GetCategoryAsync(CategoryId, token);
            return await Task.Run(() =>
            {
                return View("AddCategory", CategoryModel);
            }, token);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveCategory(TourPackageCategoryDto category, CancellationToken token)
        {
            if (!ModelState.IsValid)
                return View("AddCategory", category);
            category.CreatedOn = DateTime.Now;
            category.UpdatedOn = DateTime.Now;
            category.CreatedBy = base.GetUserName();
            category.UpdatedBy = base.GetUserName();
            if (category.Id.HasValue)
            {
                await _packageCategoryService.UpdateCategoryAsync(category, token);
            }
            else
            {
                await _packageCategoryService.CreateCategoryAsync(category, token);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeletePackageCategory(int categoryId, CancellationToken token)
        {
            var result = await _packageCategoryService.DeleteCategoryAsync(categoryId, token);

            if (result > 0)
                return Json(new { success = true, message = "Category deleted successfully" });

            return Json(new { success = false, message = "Category not found" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetAllPackageCategories([FromBody] DataTableAjaxPostModel request, CancellationToken token)
        {
            var tourPackages = await _packageCategoryService.GetTourPackageCategory(token);
            return Json(new
            {
                draw = request.draw == 0 ? 1 : request.draw,
                recordsFiltered = tourPackages.Count(),
                recordsTotal = tourPackages.Count(),
                data = tourPackages.Select(x => new
                {
                    x.Id,
                    x.CategoryName,
                    x.NoOfPackages,
                    x.IsActive
                }).ToArray()
            });

        }

        public async Task<IActionResult> ExportAll(CancellationToken token)
        {
            var data = await _packageCategoryService.ExportAllAsync(token); // fetch unpaginated filtered data
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Package Category");
            worksheet.Cell(1, 1).InsertTable(data);
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "DriversList.xlsx");
        }
        public async Task<IActionResult> ImportCategory(IFormFile file, CancellationToken token)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            List<TourPackageCategoryDto> categories;

            using var stream = file.OpenReadStream();

            if (extension == ".csv")
            {
                categories = await _readerService.ReadAsync<TourPackageCategoryDto>(stream, FileType.Csv);
            }
            else if (extension == ".xlsx" || extension == ".xls")
            {
                categories = await _readerService.ReadAsync<TourPackageCategoryDto>(stream, FileType.Excel);
            }
            else
            {
                return BadRequest("Unsupported file format. Only CSV or Excel allowed.");
            }

            await _packageCategoryService.ImportPackageCategoriesAsync(null, token);

            return RedirectToAction("Index");
        }
    }
}
