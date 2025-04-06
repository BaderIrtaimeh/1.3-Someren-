using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Someren.Models;
using Someren.Repositories;

namespace Someren.Controllers
{
   
        public class DrinkOrderController : Controller
        {
            private readonly IDrinkOrderRepository _drinkOrderRepo;
            private readonly IStudentRepository _studentRepo;
            private readonly IDrinkRepository _drinkRepo;

            public DrinkOrderController(
                IDrinkOrderRepository drinkOrderRepo,
                IStudentRepository studentRepo,
                IDrinkRepository drinkRepo)
            {
                _drinkOrderRepo = drinkOrderRepo;
                _studentRepo = studentRepo;
                _drinkRepo = drinkRepo;
            }

            public IActionResult Index()
            {
                var viewModel = new DrinkOrderViewModel

                {
                    Students = _studentRepo.GetAll(),
                    Drinks = _drinkRepo.GetAllDrinks()
                };

                return View(viewModel);
            }

            [HttpPost]
            public IActionResult Add(DrinkOrder order)
            {
                if (ModelState.IsValid)
                {
                    _drinkOrderRepo.AddDrinkOrder(order);
                    TempData["Confirmation"] = $"Order processed: {order.Quantity}x drink(s) for student {order.StudentNumber}.";
                    return RedirectToAction("Index");
                }

                
                var viewModel = new DrinkOrderViewModel
                {
                    Students = _studentRepo.GetAll(),
                    Drinks = _drinkRepo.GetAllDrinks()
                };

                return View("Index", viewModel);
            }
        }
    }

