using System.Collections.Generic;

namespace DataAccess
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
