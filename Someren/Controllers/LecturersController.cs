using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Someren.Models;
using Someren.Repositories;

namespace Someren.Controllers
{
    public class LecturersController : Controller
    {
        private readonly ILecturerRepository _repo;
        private readonly IActivityRepository _activityRepo;

        public LecturersController(ILecturerRepository repo, IActivityRepository activityRepo)
        {
            _repo = repo;
            _activityRepo = activityRepo; 
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


        public IActionResult Participating(int id)
        {
            var lecturers = _repo.GetParticipatingLecturers(id);
            ViewBag.ActivityId = id;
            return View(lecturers);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
		public IActionResult RemoveFromActivity(int lecturerId, int activityId)
		{
			_repo.RemoveLecturerFromActivity(lecturerId);

			return RedirectToAction("Participating", new { id = activityId });
		}
        public IActionResult Available()
        {
            var availableLecturers = _repo.GetAvailableLecturers();
            ViewBag.Activities = _activityRepo.GetAll(); 
            return View(availableLecturers);
        }
        [HttpPost]
        [HttpPost]
        public IActionResult AssignToActivity(int lecturerId, int activityId)
        {
            _repo.AssignLecturerToActivity(lecturerId, activityId);
            return RedirectToAction("Assign", new { id = activityId }); 
        }


        public IActionResult Assign(int id) 
        {
            var availableLecturers = _repo.GetAvailableLecturers();
            ViewBag.ActivityId = id;
            return View(availableLecturers);
        }



    }
}
