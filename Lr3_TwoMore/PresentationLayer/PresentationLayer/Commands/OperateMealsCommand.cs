using BuisnessLogic;
using DataLayer;

namespace PresentationLayer.Commands
{
    public class OperateMealsCommand : Command
    {
        public override void Execute()
        {
            Console.WriteLine("Meal editor:\n" +
                "1. See all meals\n" +
                "2. Edit meal\n" +
                "3. Add meal\n" +
                "4. Remove meal");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    {
                        for (int i = 0; i < MealController.Instance.Meals.Count; i++)
                        {
                            var meal = MealController.Instance.Meals[i];
                            Console.WriteLine($"{i + 1} {meal.Name}");
                        }
                        break;
                    }
                case "2":
                    {
                        var selectedMenu = SelectMenu();
                        (new EditSelectedMealCommand()).Execute(selectedMenu);
                        break;
                    }
                case "3":
                    {
                        var isNameCorrect = false;
                        var name = string.Empty;
                        while (isNameCorrect == false)
                        {
                            Console.Write("Enter men menu name: ");
                            name = Console.ReadLine();
                            isNameCorrect = MealController.Instance.CanAddMeal(name);
                            if (isNameCorrect == false)
                                Console.WriteLine("Incorrect name");
                        }

                        MealController.Instance.AddMeal(name);
                        Console.WriteLine("Menu successfully added");
                        break;
                    }
                case "4":
                    {
                        var selectedMenu = SelectMenu();
                        MealController.Instance.RemoveMeal(selectedMenu);
                        break;
                    }
                case "b":
                    {
                        return;
                    }
                default:
                    {
                        Console.WriteLine("Incorrent input");
                        break;
                    }
            }
        }

        private Meal SelectMenu()
        {
            var allMeals = MealController.Instance.Meals;
            for (int i = 0; i < allMeals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {allMeals[i].Name}");
            }
            var isSelectedCorrectly = false;
            var menuIndex = -1;
            while (isSelectedCorrectly == false)
            {
                Console.Write("Select menu to exit: ");
                var indexStr = Console.ReadLine();
                isSelectedCorrectly = int.TryParse(indexStr, out menuIndex);
                if (isSelectedCorrectly == false)
                {
                    continue;
                }
                menuIndex--;
                isSelectedCorrectly = menuIndex >= 0 && menuIndex < allMeals.Count;
            }

            var selectedMenu = allMeals[menuIndex];
            return selectedMenu;
        }
    }
}
