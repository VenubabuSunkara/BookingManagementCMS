using CMS.Models;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using System.Security.Cryptography;

namespace CMS.Controllers
{
    public class PramotionController(IPramotionRepository _pramotionRepository) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var pramotions = await _pramotionRepository.GetAllPramotinsAsync(cancellationToken);
            return View(pramotions);
        }

        // GET: PramotionController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: PramotionController/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: PramotionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PramotionViewModel pramotionViewModel, CancellationToken token)
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

            //Bing promotion info
            CouponCode pramotionDetails = new()
            {
                Name = pramotionViewModel.Name ?? string.Empty,
                Code = pramotionViewModel.Code ?? string.Empty,
                ValidityFrom = pramotionViewModel.ValidityFrom != null ? DateOnly.FromDateTime(pramotionViewModel.ValidityFrom.Value) : null,
                ValidityTo = pramotionViewModel.ValidityTo != null ? DateOnly.FromDateTime(pramotionViewModel.ValidityTo.Value) : null,
                RangeMin = pramotionViewModel.RangeMin,
                RangeMax = pramotionViewModel.RangeMax,
                MediaUrl = pramotionViewModel.MediaUrl ?? string.Empty,
                IsActive = pramotionViewModel.IsActive,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            var pramotionCreationStatus = await _pramotionRepository.CreatePramotionAsync(pramotionDetails, token);
            return await Task.Run(() =>
            {
                return RedirectToAction(nameof(Create));
            }, token);
        }

        // GET: PramotionController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: PramotionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
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
