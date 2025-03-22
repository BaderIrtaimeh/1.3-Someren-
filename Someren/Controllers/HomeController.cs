using Microsoft.AspNetCore.Mvc;
using Someren.Models;
using System.Diagnostics;
using Activity = System.Diagnostics.Activity;

namespace Someren.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Students()
        {
            var students = new List<Student> { };
            return View(students); 
        }
    }

}
