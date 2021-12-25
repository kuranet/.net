using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public class OrderRecord
    {
        private List<MealRecord> _meals = new List<MealRecord>();
        public int OrderNumber { get; set; }
        public List<MealRecord> Meals => _meals;
        public decimal Price => Meals.Sum(m => m.Meal.Price * m.Count);
    }
}
