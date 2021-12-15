using System.Collections.Generic;

namespace DataAccess
{
    public class Menu
    {
        public int Id { get; set; }
        public List<Meal> Meals { get; set; } = new List<Meal>();
    }
}
