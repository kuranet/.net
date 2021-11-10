using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Meal
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public void AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
        }
    }
}
