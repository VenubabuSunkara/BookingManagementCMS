using Booking.Application.Interfaces;
using Booking.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ReviewController(IReviewCommentService reviewCommentService, ILogger<ReviewController> logger) : BaseController
    {
        private readonly IReviewCommentService _reviewCommentService = reviewCommentService;
        private readonly ILogger<ReviewController> _logger = logger;
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() =>
            {
                return View();
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetAllReviews([FromBody] DataTableAjaxPostModel request, CancellationToken token)
        {
            try
            {
                string search = "";
                if (!String.IsNullOrEmpty(request.search?.value))
                    search = request.search?.value ?? string.Empty;
                var result = await _reviewCommentService.GetAllReviewComments(search, request.length, request.start, token);
                return Json(new
                {
                    draw = request.draw == 0 ? 1 : request.draw,
                    recordsFiltered = result.Filtered,
                    recordsTotal = result.Total,
                    data = result.ReviewComments.ToArray()
                });
            }
            catch (Exception ex)
            {
                return Json("Something went wrong {0}", ex);
            }
        }
        public async Task<IActionResult> GetReviewsByVehicleDriver()
        {
            return await Task.Run(() =>
            {
                return View("Index");
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetReviewsByVehicleDriver(int VehicleId, int DriverId, CancellationToken token)
        {
            var comments = await _reviewCommentService.GetAllVehicleDriverReviewsAsync(DriverId, VehicleId, token);
            return View("DriverVehicleReview", comments);
        }
    }
}
