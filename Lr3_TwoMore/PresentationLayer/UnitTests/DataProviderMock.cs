using DataLayer;
using System.Collections.Generic;

namespace UnitTests
{
    internal class DataProviderMock : IDataProvider
    {
        public IList<Ingredient> Ingredients { get; set; }

        public IList<Meal> Meals { get; set; }

        public IList<Menu> Menus { get; set; }

        public DataProviderMock()
        {
            Ingredients = new List<Ingredient>()
        {
            new Ingredient() { Name = "ingr1" },
            new Ingredient() { Name = "ingr2" },
            new Ingredient() { Name = "ingr3" },
            new Ingredient() { Name = "ingr4" },
            new Ingredient() { Name = "ingr5" },
        };

            Meals = new List<Meal>()
        {
            new Meal() { Name = "meal1", Price = 1, Ingredients = new List<Ingredient>{ Ingredients [0]} },
            new Meal() { Name = "meal2", Price = 2, Ingredients = new List<Ingredient>{ Ingredients [1]} },
            new Meal() { Name = "meal3", Price = 3, Ingredients = new List<Ingredient>{ Ingredients [2]} },
            new Meal() { Name = "meal4", Price = 4, Ingredients = new List<Ingredient>{ Ingredients [3]} },
            new Meal() { Name = "meal5", Price = 5, Ingredients = new List<Ingredient>{ Ingredients [4]} },
        };
            Menus = new List<Menu>()
        {
            new Menu() { Name= "menu1", Meals = new List<Meal>{ Meals[0], Meals[1]}, },
            new Menu() { Name= "menu2", Meals = new List<Meal>{ Meals[2], Meals[3], Meals[4]}, }
        };
        }
    }
}
