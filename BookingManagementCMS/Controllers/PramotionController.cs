using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class PramotionController : Controller
    {
        // GET: PramotionController
        public IActionResult Index()
        {
            return View();
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
        public IActionResult Create(IFormCollection collection)
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
