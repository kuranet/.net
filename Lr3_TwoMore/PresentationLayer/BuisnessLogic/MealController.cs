using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic
{
    public class MealController
    {
        private static MealController instance;

        private MealController()
        { }

        public static MealController Instance
        {
            get
            {
                if (instance == null)
                    instance = new MealController();
                return instance;
            }
        }

        public IList<Meal> Meals => DataProvider.Meals.GetAll().ToList();

        private IDataProvider DataProvider { get; set; }

        public void Initialize(IDataProvider dataProvider)
        {
            DataProvider = dataProvider;
        }

        public bool CanAddMeal(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            var menuWithSuchName = Meals.FirstOrDefault(meal => meal.Name == name);
            if (menuWithSuchName != null)
                return false;

            return true;
        }

        public void AddMeal(string name)
        {
            if (CanAddMeal(name) == false)
                throw new ArgumentException($"Can't add meal with name {name}");

            var newMeal = new Meal()
            {
                Name = name,
            };

            DataProvider.Meals.Create(newMeal);
        }

        public bool CanRemoveMeal(Meal meal)
        {
            // Unknown meal.
            if (CanOperateMeal(meal) == false)
                return false;

            return true;
        }

        public void RemoveMeal(Meal meal)
        {
            if (CanRemoveMeal(meal) == false)
                throw new ArgumentException($"Can't remove meal {meal.Name}");

            DataProvider.Meals.Delete(meal.Id);
        }

        public bool CanAddIngredientToMeal(Meal meal, Ingredient ingr)
        {
            // Meal doesn't exists in database.
            if (CanOperateMeal(meal) == false)
                return false;

            // Ingredient doesn't exists in database.
            if (IngredientController.Instance.CanOperateIngredient(ingr) == false)
                return false;

            // Meal contains ingredient.
            if (meal.Ingredients.Contains(ingr))
                return false;

            return true;
        }

        public void AddIngredientToMeal(Meal meal, Ingredient ingredient)
        {
            if (CanAddIngredientToMeal(meal, ingredient) == false)
                throw new ArgumentException($"Can't add ingredient {ingredient.Name} to meal {meal.Name}");

            meal.Ingredients.Add(ingredient);
        }

        public bool CanRemoveIngredientFromMeal(Meal meal, Ingredient ingredient)
        {
            // Meal doesn't exists in database.
            if (CanOperateMeal(meal) == false)
                return false;

            // Ingredient doesn't exists in database.
            if (IngredientController.Instance.CanOperateIngredient(ingredient) == false)
                return false;

            // Meal doesn't contains ingredient.
            if (meal.Ingredients.Any(i => i.Id == ingredient.Id) == false)
                return false;

            return true;
        }

        public void RemoveIngredientFromMeal(Meal meal, Ingredient ingredient)
        {
            if (CanRemoveIngredientFromMeal(meal, ingredient) == false)
                throw new ArgumentException($"Can't remove ingredient {ingredient.Name} from meal {meal.Name}");

            var iToRemove = meal.Ingredients.First(i => i.Id == ingredient.Id);
            meal.Ingredients.Remove(iToRemove);
        }

        public bool CanOperateMeal(Meal meal)
            => Meals.Any(m => m.Id == meal.Id);
    }
}
