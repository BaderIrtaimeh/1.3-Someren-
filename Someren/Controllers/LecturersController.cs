using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Someren.Models;
using Someren.Repositories;

namespace Someren.Controllers
{
    public class LecturersController : Controller
    {
        private readonly ILecturerRepository _repo;

        public LecturersController(ILecturerRepository repo)
        {
            _repo = repo;
        }
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

        public IActionResult Edit(int id)
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
        public IActionResult Delete(int id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
