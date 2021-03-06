using DataLayer;

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

        public IList<Menu> Menus => DataProvider.Menus.GetAll().ToList();

        public void Initialize(IDataProvider provider)
        {
            DataProvider = provider;
        }

        public Menu GetMenu(string name)
            => Menus.FirstOrDefault(m => m.Name == name);

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
                throw new ArgumentException($"Can't add menu with name {name}");

            var newMeal = new Menu()
            {
                Name = name,
            };

            DataProvider.Menus.Create(newMeal);
        }

        public bool CanRemoveMenu(Menu menu)
        {
            // Unknown meal.
            if (CanOperateWithMenu(menu) == false)
                return false;

            return true;
        }

        public void RemoveMenu(Menu menu)
        {
            if (CanRemoveMenu(menu) == false)
                throw new ArgumentException($"Can't remove menu {menu.Name}");

            DataProvider.Menus.Delete(menu.Id);
        }

        public bool CanAddMealToMenu(Menu menu, Meal meal)
        {
            // Menu doesn't exist in database.
            if (CanOperateWithMenu(menu) == false)
                return false;

            // Meal doesn't exist in database.
            if (MealController.Instance.CanOperateMeal(meal) == false)
                return false; ;

            // Menu also contains meal.
            if (menu.Meals.Contains(meal))
                return false;

            return true;
        }

        public void AddMealToMenu(Menu menu, Meal meal)
        {
            if (CanAddMealToMenu(menu, meal) == false)
                throw new ArgumentException($"Can't add {meal.Name} to menu {menu.Name}");

            menu.Meals.Add(meal);
        }

        public bool CanRemoveMealFromMenu(Menu menu, Meal meal)
        {
            // Menu doesn't exist in database.
            if (CanOperateWithMenu(menu) == false)
                return false;

            // Meal doesn't exist in database.
            if (MealController.Instance.CanOperateMeal(meal) == false)
                return false;

            // Menu doesn't contains meal.
            if (menu.Meals.Any(m => m.Id == meal.Id) == false)
                return false;

            return true;
        }

        public void RemoveMealFromMenu(Menu menu, Meal meal)
        {
            if (CanRemoveMealFromMenu(menu, meal) == false)
                throw new ArgumentException($"Can't remove meal {meal.Name} from menu { menu.Name }");

            var mToRemove = menu.Meals.First(m => m.Id == meal.Id);
            menu.Meals.Remove(mToRemove);
        }

        private bool CanOperateWithMenu(Menu menu)
            => Menus.Contains(menu);
    }
}
