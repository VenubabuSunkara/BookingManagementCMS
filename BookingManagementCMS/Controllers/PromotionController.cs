using CMS.Models;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Common;
using Repository;
using Repository.Interfaces;
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
            var result = await _dataTableRepository.GetDataAsync<CouponCode>(promotions, request);
            return Json(result);
        }

        /// <summary>
        /// Check promotion specific filed existance
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyCouponCode(string code)
        {
            if (await _pramotionRepository.FindPramotionAsync(c => c.Code == code, CancellationToken.None))
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
                ValidityFrom = pramotionViewModel.ValidityFrom,
                ValidityTo = pramotionViewModel.ValidityTo,
                RangeMin = pramotionViewModel.RangeMin,
                RangeMax = pramotionViewModel.RangeMax,
                MediaUrl = pramotionViewModel.MediaUrl ?? string.Empty,
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
                ValidityFrom = promotion.ValidityFrom,
                ValidityTo = promotion.ValidityTo,
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
                ValidityFrom = pramotionViewModel.ValidityFrom,
                ValidityTo = pramotionViewModel.ValidityTo,
                RangeMin = pramotionViewModel.RangeMin,
                RangeMax = pramotionViewModel.RangeMax,
                MediaUrl = pramotionViewModel.MediaUrl ?? string.Empty,
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

        // GET: PramotionController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: PramotionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
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
