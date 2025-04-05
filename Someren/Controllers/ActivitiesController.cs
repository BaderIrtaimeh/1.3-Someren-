using Microsoft.AspNetCore.Mvc;
using Someren.Repositories;

namespace Someren.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly IActivityRepository _repo;

        public ActivitiesController(IActivityRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var activities = _repo.GetAll();
            return View(activities);
        }
    }
}
