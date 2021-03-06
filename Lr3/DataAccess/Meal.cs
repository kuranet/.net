using System.Collections.Generic;

namespace DataAccess
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<int> IngredientIds { get; set; } = new List<int>();
    }
}
