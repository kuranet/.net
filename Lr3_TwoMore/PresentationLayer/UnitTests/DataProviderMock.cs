using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    internal class DataProviderMock : IDataProvider
    {
        private IRepository<Menu> _menus;
        private IRepository<Meal> _meals;
        private IRepository<Ingredient> _ingredients;

        public IRepository<Menu> Menus => _menus;

        public IRepository<Meal> Meals => _meals;

        public IRepository<Ingredient> Ingredients => _ingredients;

        public void Dispose()
        {
        }

        public DataProviderMock()
        {
            _menus = new MenuRepMock();
            _ingredients = new IngrRepMock();
            _meals = new MealsRepMock();
        }

        private class MenuRepMock : IRepository<Menu>
        {
            private List<Menu> _menus = new List<Menu>()
            {
            new Menu() { Name = "menu1", Meals = new List<Meal>(), },
            new Menu() { Name = "menu2", Meals = new List<Meal>(), }
            };

            public void Create(Menu item)
            {
                _menus.Add(item);
            }

            public void Delete(int id)
            {
                var menu = Get(id);
                if (menu != null)
                    _menus.Remove(menu);
            }

            public IEnumerable<Menu> Find(Func<Menu, bool> predicate)
                => _menus.Where(x => predicate(x));

            public Menu Get(int id) => _menus.FirstOrDefault(m => m.Id == id);

            public IEnumerable<Menu> GetAll() => _menus;

            public void Update(Menu item)
            {
            }
        }

        private class MealsRepMock : IRepository<Meal>
        {
            private List<Meal> _menus = new List<Meal>()
            {
            new Meal() { Name = "meal1", Ingredients = new List<Ingredient>(), },
            new Meal() { Name = "meal2", Ingredients = new List<Ingredient>(), }
            };

            public void Create(Meal item)
            {
                _menus.Add(item);
            }

            public void Delete(int id)
            {
                var menu = Get(id);
                if (menu != null)
                    _menus.Remove(menu);
            }

            public IEnumerable<Meal> Find(Func<Meal, bool> predicate)
                => _menus.Where(x => predicate(x));

            public Meal Get(int id) => _menus.FirstOrDefault(m => m.Id == id);

            public IEnumerable<Meal> GetAll() => _menus;

            public void Update(Meal item)
            {
            }
        }
        private class IngrRepMock : IRepository<Ingredient>
        {
            private List<Ingredient> _menus = new List<Ingredient>()
            {
            new Ingredient() { Name = "ingredient1" },
            new Ingredient() { Name = "ingredient2" }
            };

            public void Create(Ingredient item)
            {
                _menus.Add(item);
            }

            public void Delete(int id)
            {
                var menu = Get(id);
                if (menu != null)
                    _menus.Remove(menu);
            }

            public IEnumerable<Ingredient> Find(Func<Ingredient, bool> predicate)
                => _menus.Where(x => predicate(x));

            public Ingredient Get(int id) => _menus.FirstOrDefault(m => m.Id == id);

            public IEnumerable<Ingredient> GetAll() => _menus;

            public void Update(Ingredient item)
            {
            }
        }
    }
}
