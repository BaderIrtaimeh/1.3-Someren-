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
            return View("Index", "Students");
        }

        public IActionResult Students()
        {
            var students = new List<Student>
        {
            new Student { StudentNumber = 1, FirstName = "John", LastName = "Doe", PhoneNumber = "123-456-7890", Class = "A", AmountDrinkConsumed = "23" },
            new Student { StudentNumber = 2, FirstName = "Jane", LastName = "Smith", PhoneNumber = "987-654-3210", Class = "B", AmountDrinkConsumed = "2" },
            new Student { StudentNumber = 3, FirstName = "Mike", LastName = "Johnson", PhoneNumber = "555-555-5555", Class = "C", AmountDrinkConsumed = "10" }
        };

            return View(students); 
        }
    }

}
