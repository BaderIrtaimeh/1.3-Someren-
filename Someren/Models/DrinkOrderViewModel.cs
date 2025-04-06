using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Someren.Models
{
    public class DrinkOrderViewModel 
    {
        public DrinkOrder Order { get; set; }
        public List<Student> Students { get; set; }
        public List<Drink> Drinks { get; set; }
    }
}
