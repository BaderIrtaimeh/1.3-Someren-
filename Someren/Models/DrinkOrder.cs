using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Someren.Models
{
    public class DrinkOrder 
    {
        public int OrderID { get; set; }
        public string StudentNumber { get; set; }
        public int DrinkID { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
