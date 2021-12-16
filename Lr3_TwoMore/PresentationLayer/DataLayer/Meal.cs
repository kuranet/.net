using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int? MealId { get; set; }

        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public Meal()
        {
            Ingredients = new List<Ingredient>();
        }
    }
}
