using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Someren.Models;

namespace Someren.Controllers
{
    public class LecturersController : Controller
    {
        public IActionResult Index()
        {
            var lecturers = _repo.GetAll();
            return View(lecturers);
        }

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(lecturer);
                return RedirectToAction("Index");
            }
            return View(lecturer);
        }

        public IActionResult Edit(string id)
        {
            var lecturer = _repo.GetById(id);
            if (lecturer == null) return NotFound();
            return View(lecturer);
        }

        [HttpPost]
        public IActionResult Edit(Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(lecturer);
                return RedirectToAction("Index");
            }
            return View(lecturer);
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
