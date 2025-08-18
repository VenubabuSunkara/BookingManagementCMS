using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Web.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Booking.Web.Controllers
{
    public class CouponCodeController(ICouponCodeService couponCodeService) : BaseController
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
            return await Task.Run(() => View(), token);
        }

        /// <summary>
        /// Get couponcodes list items
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoadData(DataTableRequestDto request, CancellationToken cancellationToken)
        {
            var couponCodes = await _couponCodeService.GetAllCouponCodesAsync(request, [], cancellationToken);
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
        public async Task<IActionResult> VerifyCouponCode(string code, int couponCodeId)
        {
            if (await _couponCodeService.FindCouponCodeAsync(couponCodeId, code, CancellationToken.None))
                return Json($"Coupon code {code} is already in use.");

            return Json(true);
        }

        /// <summary>
        /// Display Create promotion form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return View("Index");
                }, token);

            return await Task.Run(() =>
            {
                return View("Create", new CouponCodeDto());
            }, token);
        }

        /// <summary>
        /// Save promotion details
        /// </summary>
        /// <param name="pramotionViewModel"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CouponCodeDto couponCodeDto, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return RedirectToAction(nameof(Index));
                }, token);

            if (!ModelState.IsValid)
                return await Task.Run(() =>
                {
                    return View();
                }, token);

            //upload the file and bind url
            couponCodeDto.MediaUrl = couponCodeDto.FileUpload != null && couponCodeDto.FileUpload.Length > 0
                ? await FileUpload.UploadFileAsync(couponCodeDto.FileUpload, "CouponCode", token)
                : string.Empty;

            var promotionCreationStatus = await _couponCodeService.CreateCouponCodeAsync(couponCodeDto, token);
            TempData["couponCodeSuccessMessage"] = promotionCreationStatus ? "Coupon code created successfully."
                                                                          : "Unable to create Coupon code.Please try again after some time.";
            return await Task.Run(() =>
            {
                return RedirectToAction(nameof(Index));
            }, token);
        }

        /// <summary>
        /// Display update form
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return RedirectToAction(nameof(Index));
                }, token);

            var couponCodeInfo = await _couponCodeService.GetCouponCodeByIdAsync(id, token);
            if (couponCodeInfo == null)
            {
                TempData["couponCodeSuccessMessage"] = "Internal server error.Please try again after some time.";
                return await Task.Run(() =>
                {
                    return RedirectToAction(nameof(Index));
                }, token);
            }

            return await Task.Run(() => View(nameof(Edit), couponCodeInfo), token);
        }

        /// <summary>
        /// Update the promotion details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CouponCodeDto couponCodeDto, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return RedirectToAction(nameof(Index));
                }, token);

            if (!ModelState.IsValid)
                return await Task.Run(() =>
                {
                    return View();
                }, token);

            //upload file and bind the url
            couponCodeDto.MediaUrl = couponCodeDto.FileUpload != null && couponCodeDto.FileUpload.Length > 0
                ? await FileUpload.UploadFileAsync(couponCodeDto.FileUpload, "CouponCode", token)
                : couponCodeDto.MediaUrl ?? string.Empty;

            var promotionUpdateStatus = await _couponCodeService.UpdateCouponCodeAsync(couponCodeDto, token);
            TempData["couponCodeSuccessMessage"] = promotionUpdateStatus ? "Coupn Code updated successfully."
                                                                          : "Unable to updated Coupon Code.Please try again after some time.";
            return await Task.Run(() =>
            {
                return RedirectToAction(nameof(Index));
            }, token);
        }

        /// <summary>
        /// Display promotion information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Details(int id, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return RedirectToAction(nameof(Index));
                }, token);

            var couponCodeInfo = await _couponCodeService.GetCouponCodeByIdAsync(id, token);
            if (couponCodeInfo == null) return await Task.Run(() => RedirectToAction(nameof(Index)), token);

            return View("Details", couponCodeInfo);
        }

        /// <summary>
        /// Cancel the promotion event
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Cancel(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return RedirectToAction(nameof(Index));
            }, token);
        }


        /// <summary>
        /// Delete promotion
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            bool delValue = await _couponCodeService.DeleteCouponCodeAsync(id, cancellationToken);
            return Json(new { delValue });
        }
    }
}
