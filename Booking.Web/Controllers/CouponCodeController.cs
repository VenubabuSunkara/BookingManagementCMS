using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    public class CouponCodeController : Controller
    {
        // GET: CouponCodeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CouponCodeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CouponCodeController/Create
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
