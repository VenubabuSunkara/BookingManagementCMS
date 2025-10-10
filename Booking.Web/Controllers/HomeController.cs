using Booking.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Booking.Web.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class HomeController : BaseController
    {
        private readonly ICustomerService _customerService;
        private readonly IBookingService _bookingService;
        private readonly IReviewCommentService _reviewComment;
        private readonly IPackageService _packageService;

        public HomeController(ICustomerService customerService, IBookingService bookingService, IReviewCommentService reviewComment, IPackageService packageService)
        {
            _customerService = customerService;
            _bookingService = bookingService;
            _reviewComment = reviewComment;
            _packageService = packageService;
        }
        public async Task<IActionResult> Index()
        {


            return await Task.Run(() => View());
        }
    }
}
