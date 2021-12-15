using BuisnessLogic;
using DataLayer;

namespace PresentationLayer.Commands
{
    public class EditSelectedMenuCommand : Command<Menu>
    {
        public override void Execute(Menu selectedMenu)
        {
            var input = string.Empty;
            while (input != "b")
            {
                var canRemoveMeal = selectedMenu.Meals.Any();
                Console.WriteLine("Operations you can perform:\n" +
                    "1. See meals\n" +
                    "2. Add meal");
                if (canRemoveMeal)
                    Console.WriteLine("3. Remove meal");

                Console.WriteLine("b. Back");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        {
                            for (int i = 0; i < selectedMenu.Meals.Count; i++)
                            {
                                Console.WriteLine($"{i+1}. {selectedMenu.Meals[i].Name}");
                            }
                            break;
                        }
                    case "2":
                        {
                            var allMeals = MealController.Instance.Meals
                                .Where(meal => MenuController.Instance.CanAddMealToMenu(selectedMenu, meal))
                                .ToList();

                            var selectedMeal = SelectMeal(allMeals);

                            MenuController.Instance.AddMealToMenu(selectedMenu, selectedMeal);

                            break;
                        }
                    case "3":
                        {
                            if (canRemoveMeal == false)
                                return;

                            var mealInMenu = selectedMenu.Meals;
                            var selectedMeal = SelectMeal(mealInMenu);

                            MenuController.Instance.RemoveMealFromMenu(selectedMenu, selectedMeal);

                            break;
                        }
                    case "b":
                        {
                            return;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong input");
                            break;
                        }
                }
            }
        }
        private Meal SelectMeal(IList<Meal> meals)
        {
            for (int i = 0; i < meals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {meals[i].Name}");
            }
            var isSelectedCorrectly = false;
            var menuIndex = -1;
            while (isSelectedCorrectly == false)
            {
                Console.Write("Select meal: ");
                var indexStr = Console.ReadLine();
                isSelectedCorrectly = int.TryParse(indexStr, out menuIndex);
                if (isSelectedCorrectly == false)
                {
                    continue;
                }
                menuIndex--;
                isSelectedCorrectly = menuIndex >= 0 && menuIndex < meals.Count;
            }

            var selectedMenu = meals[menuIndex];
            return selectedMenu;
        }
    }
}
