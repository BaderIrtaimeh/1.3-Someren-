using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Someren.Controllers
{
    public class LecturersController : Controller
    {
        // GET: LecturersController
        public ActionResult Index()
        {
            return View();
        }

        // GET: LecturersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LecturersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LecturersController/Create
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

        // GET: LecturersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LecturersController/Edit/5
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

        // GET: LecturersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LecturersController/Delete/5
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
