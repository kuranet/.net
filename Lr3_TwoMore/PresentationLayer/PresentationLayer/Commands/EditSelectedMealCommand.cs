using BuisnessLogic;
using DataLayer;

namespace PresentationLayer.Commands
{
    public class EditSelectedMealCommand : Command<Meal>
    {
        public override void Execute(Meal selectedMeal)
        {
            var input = string.Empty;
            while (input != "b")
            {
                var canRemoveMeal = selectedMeal.Ingredients.Any();
                Console.WriteLine("Operations you can perform:\n" +
                    "1. See ingredients\n" +
                    "2. Add ingredient");
                if (canRemoveMeal)
                    Console.WriteLine("3. Remove ingredient");

                Console.WriteLine("b. Back");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        {
                            for (int i = 0; i < selectedMeal.Ingredients.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {selectedMeal.Ingredients[i].Name}");
                            }
                            break;
                        }
                    case "2":
                        {
                            var allMeals = IngredientController.Instance.Ingredients
                                .Where(meal => MealController.Instance.CanAddIngredientToMeal(selectedMeal, meal))
                                .ToList();

                            var selectedIngredient = SelectIngredient(allMeals);

                            MealController.Instance.AddIngredientToMeal(selectedMeal, selectedIngredient);

                            break;
                        }
                    case "3":
                        {
                            if (canRemoveMeal == false)
                                return;

                            var ingredientsInMeal = selectedMeal.Ingredients;
                            var selectedIngredient = SelectIngredient(ingredientsInMeal);

                            MealController.Instance.RemoveIngredientFromMeal(selectedMeal, selectedIngredient);

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
        private Ingredient SelectIngredient(IList<Ingredient> ingredient)
        {
            for (int i = 0; i < ingredient.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ingredient[i].Name}");
            }
            var isSelectedCorrectly = false;
            var menuIndex = -1;
            while (isSelectedCorrectly == false)
            {
                Console.Write("Select ingredient: ");
                var indexStr = Console.ReadLine();
                isSelectedCorrectly = int.TryParse(indexStr, out menuIndex);
                if (isSelectedCorrectly == false)
                {
                    continue;
                }
                menuIndex--;
                isSelectedCorrectly = menuIndex >= 0 && menuIndex < ingredient.Count;
            }

            var selectedMenu = ingredient[menuIndex];
            return selectedMenu;
        }
    }
}
