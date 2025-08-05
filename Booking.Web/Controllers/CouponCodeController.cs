using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Booking.Web.Controllers
{
    public class CouponCodeController(ICouponCodeService couponCodeService) : Controller
    {
        private readonly ICouponCodeService _couponCodeService = couponCodeService;
        /// <summary>
        /// Display all couponcodes
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index(CancellationToken token)
        {
            return await Task.Run(() => View(),token);
        }

        /// <summary>
        /// Get couponcodes list items
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> LoadData(DataTableRequestDto request, CancellationToken cancellationToken)
        {
            var couponCodes = await _couponCodeService.GetAllCouponCodesAsync(request,[],cancellationToken);
            return Json(couponCodes);
        }

        /// <summary>
        ///  Check the couponcode existance
        /// </summary>
        /// <param name="code"></param>
        /// <param name="promotionId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyCouponCode(string couponCode, int couponCodeId)
        {
            if (await _couponCodeService.FindCouponCodeAsync(couponCodeId, couponCode, CancellationToken.None))
                return Json($"Coupon code {couponCode} is already in use.");

            return Json(true);
        }

        /// <summary>
        /// Display Create promotion form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CouponCodeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CouponCodeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CouponCodeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CouponCodeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CouponCodeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
