using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Someren.Models;

namespace Someren.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(ILogger<StudentsController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Students()
        {
            // Mock data directly inside the controller
            var students = new List<Student>
            {
                new Student { StudentNumber = 1, FirstName = "John", LastName = "Doe", PhoneNumber = "123-456-7890", Class = "A", AmountDrinkConsumed = "23" },
                new Student { StudentNumber = 2, FirstName = "Jane", LastName = "Smith", PhoneNumber = "987-654-3210", Class = "B", AmountDrinkConsumed = "2" },
                new Student { StudentNumber = 3, FirstName = "Mike", LastName = "Johnson", PhoneNumber = "555-555-5555", Class = "C", AmountDrinkConsumed = "10" }
            };
            if (students == null)
            {
                _logger.LogError("Student list is null.");
                return View(new List<Student>());
            }

            _logger.LogInformation("Displaying student list with {count} students.", students.Count);

            return View("Index", "Students");


        }
    }
}
