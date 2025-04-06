using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Someren.Models;
using Someren.Repositories;

namespace Someren.Controllers
{
    public class DrinksController : Controller
    {
        private readonly IDrinkRepository _drinkRepo;

        public DrinksController(IDrinkRepository drinkRepo)
        {
            _drinkRepo = drinkRepo;
        }

        public IActionResult Index()
        {
            List<Drink> drinks = _drinkRepo.GetAllDrinks();
            return View(drinks);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Drink drink)
        {
            if (ModelState.IsValid)
            {
                _drinkRepo.Add(drink);
                return RedirectToAction("Index");
            }

            return View(drink);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var drink = _drinkRepo.GetById(id);
            if (drink == null)
                return NotFound();

            return View(drink);
        }

        [HttpPost]
        public IActionResult Edit(Drink drink)
        {
            if (ModelState.IsValid)
            {
                _drinkRepo.Update(drink);
                return RedirectToAction("Index");
            }

            return View(drink);
        }

     


    }
}
