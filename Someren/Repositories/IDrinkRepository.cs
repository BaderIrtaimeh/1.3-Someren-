
using Someren.Models;
namespace Someren.Repositories
{
    public interface IDrinkRepository
    {
        List<Drink> GetAllDrinks();
        void Add(Drink drink);
        void Update(Drink drink);
        Drink GetById(int drinkID);
        

    }
}
