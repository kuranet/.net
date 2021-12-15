using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic
{
    public class MenuController
    {
        private static MenuController instance;

        private MenuController()
        { }

        public static MenuController Instance
        {
            get
            {
                if (instance == null)
                    instance = new MenuController();
                return instance;
            }
        }

        private IDataProvider DataProvider { get; set; }

        public IList<Menu> Menus => DataProvider.Menus;

        public void Initialize(IDataProvider provider)
        {
            DataProvider = provider;
        }

        public Menu GetMenu(string name)
            => DataProvider.Menus.FirstOrDefault(m => m.Name == name);

        public bool CanAddMenu(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            var menuWithSuchName = GetMenu(name);
            if (menuWithSuchName != null)
                return false;

            return true;
        }

        public void AddMenu(string name)
        {
            if (CanAddMenu(name) == false)
                throw new ArgumentException($"Menu {name} doesn't exist in database");

            var newMeal = new Menu()
            {
                Name = name,
            };

            DataProvider.Menus.Add(newMeal);
        }

        public void RemoveMenu(string name)
        {
            var meal = GetMenu(name);
            if (meal == null)
                throw new ArgumentException($"Menu {name} doesn't exist in database");

            DataProvider.Menus.Remove(meal);
        }

        public bool CanAddMealToMenu(Menu menu, Meal meal)
        {
            if (CanOperateWithMenu(menu) == false)
                return false;

            if (MealController.Instance.CanOperateMeal(meal) == false)
                return false; ;

            if (menu.Meals.Contains(meal))
                return false;

            return true;
        }

        public void AddMealToMenu(Menu menu, Meal meal)
        {
            if (CanOperateWithMenu(menu) == false)
                throw new ArgumentException($"Menu {menu.Name} doesn't exist in database");

            if (MealController.Instance.CanOperateMeal(meal) == false)
                throw new ArgumentException($"Meal {meal.Name} doesn't exist in database");

            if (menu.Meals.Contains(meal))
                throw new ArgumentException($"Menu {menu.Name} also contains {meal.Name}");

            menu.Meals.Add(meal);
        }

        public void RemoveMealFromMenu(Menu menu, Meal meal)
        {
            if (CanOperateWithMenu(menu) == false)
                throw new ArgumentException($"Menu {menu.Name} doesn't exist in database");

            if (MealController.Instance.CanOperateMeal(meal) == false)
                throw new ArgumentException($"Meal {meal.Name} doesn't exist in database");

            if (menu.Meals.Contains(meal) == false)
                throw new ArgumentException($"Menu {menu.Name} doesn't contains {meal.Name}");

            menu.Meals.Remove(meal);
        }

        private bool CanOperateWithMenu(Menu menu)
            => DataProvider.Menus.Contains(menu);
    }
}
