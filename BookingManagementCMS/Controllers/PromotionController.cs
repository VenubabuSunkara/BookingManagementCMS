using CMS.Helper;
using CMS.Models;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Common;
using Repository;
using Repository.Interfaces;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    public class PromotionController(IPromotionRepository _pramotionRepository,
                                     IDataTableRepository _dataTableRepository) : Controller
    {
        /// <summary>
        /// Promotions landing page
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            return await Task.Run(() => View(), cancellationToken);
        }

        /// <summary>
        /// Get promotions list items
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> LoadData(DatatableRequest request, CancellationToken cancellationToken)
        {
            var promotions = _pramotionRepository.GetQuarablePramotionData();
            var result = await _dataTableRepository.GetDataAsync<CouponCode>(promotions, request, []);
            return Json(result);
        }

        /// <summary>
        ///  Check the couponcode existance
        /// </summary>
        /// <param name="code"></param>
        /// <param name="promotionId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyCouponCode(string code,int promotionId,CancellationToken token)
        {
            Expression<Func<CouponCode,bool>> expression = promotionId > 0 ? x => x.Id != promotionId && x.Code == code
                                                                           : x => x.Code == code;
            if (await _pramotionRepository.FindPramotionAsync(expression, token))
            {
                return Json($"Coupon code {code} is already in use.");
            }

            return Json(true);
        }

        /// <summary>
        /// Display Create promotion form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Save promotion details
        /// </summary>
        /// <param name="pramotionViewModel"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PromotionViewModel pramotionViewModel, CancellationToken token)
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

            //Bind promotion info
            CouponCode promotionDetails = new()
            {
                Name = pramotionViewModel.Name ?? string.Empty,
                Code = pramotionViewModel.Code ?? string.Empty,
                ValidityFrom = pramotionViewModel.ValidityFrom.HasValue ? DateOnly.FromDateTime(pramotionViewModel.ValidityFrom.Value) : null,
                ValidityTo = pramotionViewModel.ValidityTo.HasValue ? DateOnly.FromDateTime(pramotionViewModel.ValidityTo.Value) : null,
                RangeMin = pramotionViewModel.RangeMin,
                RangeMax = pramotionViewModel.RangeMax,
                MediaUrl = pramotionViewModel.FileUpload != null && pramotionViewModel.FileUpload.Length > 0 ? await FileUpload.UploadFileAsync(pramotionViewModel.FileUpload, "Promotions",token) : string.Empty,
                IsActive = pramotionViewModel.IsActive,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            var promotionCreationStatus = await _pramotionRepository.CreatePramotionAsync(promotionDetails, token);
            TempData["PromotionSuccessMessage"] = promotionCreationStatus ? "Promotion created successfully."
                                                                          : "Unable to create promotion.Please try again after some time.";
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

            var promotion = await _pramotionRepository.GetPramotionByIdAsync(x => x.Id == id, token);
            if (promotion == null)
            {
                TempData["PromotionSuccessMessage"] = "Internal server error.Please try again after some time.";
                return await Task.Run(() =>
                {
                    return RedirectToAction(nameof(Index));
                }, token);
            }

            PromotionViewModel promotionViewModel = new()
            {
                Name = promotion.Name,
                Code = promotion.Code,
                ValidityFrom = promotion.ValidityFrom.HasValue ? Convert.ToDateTime(Convert.ToString(promotion.ValidityFrom)) : null,
                ValidityTo = promotion.ValidityTo.HasValue ? Convert.ToDateTime(Convert.ToString(promotion.ValidityTo)) : null,
                RangeMin = promotion.RangeMin,
                RangeMax = promotion.RangeMax,
                MediaUrl = promotion.MediaUrl,
                IsActive = promotion.IsActive.HasValue && promotion.IsActive.Value,
                promotionId = promotion.Id
            };
            return View(promotionViewModel);
        }

        /// <summary>
        /// Update the promotion details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(PromotionViewModel pramotionViewModel, CancellationToken token)
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

            //Bind promotion info
            CouponCode promotionDetails = new()
            {
                Name = pramotionViewModel.Name ?? string.Empty,
                Code = pramotionViewModel.Code ?? string.Empty,
                ValidityFrom = pramotionViewModel.ValidityFrom.HasValue ? DateOnly.FromDateTime(pramotionViewModel.ValidityFrom.Value) : null,
                ValidityTo = pramotionViewModel.ValidityTo.HasValue ? DateOnly.FromDateTime(pramotionViewModel.ValidityTo.Value) : null,
                RangeMin = pramotionViewModel.RangeMin,
                RangeMax = pramotionViewModel.RangeMax,
                MediaUrl = pramotionViewModel.FileUpload != null && pramotionViewModel.FileUpload.Length > 0 ? await FileUpload.UploadFileAsync(pramotionViewModel.FileUpload, "Promotions", token) : pramotionViewModel.MediaUrl ?? string.Empty,
                IsActive = pramotionViewModel.IsActive,
                UpdatedOn = DateTime.UtcNow,
                Id = pramotionViewModel.promotionId
            };

            var promotionUpdateStatus = await _pramotionRepository.UpdatePramotionAsync(promotionDetails, token);
            TempData["PromotionSuccessMessage"] = promotionUpdateStatus ? "Promotion updated successfully."
                                                                          : "Unable to updated promotion.Please try again after some time.";
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

            var promotion = await _pramotionRepository.GetPramotionByIdAsync(x => x.Id == id, token);
            if (promotion == null) return await Task.Run(() => RedirectToAction(nameof(Index)), token);

            return View(promotion);
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
            bool delValue = await _pramotionRepository.DeletePramotionAsync(x => x.Id == id, cancellationToken);
            return Json(new { delValue });
        }
    }
}
