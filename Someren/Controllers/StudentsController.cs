using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Someren.Models;
using Someren.Repositories;

namespace Someren.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _repo;

        public StudentsController(IStudentRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var students = _repo.GetAll();
            return View(students);
        }
        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(Student student)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Delete(int studentNumber)
        {
            _repo.Delete(studentNumber);
            return RedirectToAction("Index");
        }

    }
}

