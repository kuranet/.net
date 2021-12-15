using System.Collections.Generic;

namespace DataAccess
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> MealIds { get; set; } = new List<int>();
    }
}
