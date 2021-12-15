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

        public IList<Meal> Meals => DataProvider.Meals;

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
                return;

            var hasSuchName = Meals.Any(x => x.Name == name);
            if (hasSuchName)
                throw new Exception("Such meal also exists");

            var newMeal = new Meal()
            {
                Name = name,
            };

            Meals.Add(newMeal);
        }

        public void RemoveMeal(Meal ingr)
        {
            if (CanOperateMeal(ingr) == false)
                throw new Exception($"Unknown meal {ingr.Name}");

            Meals.Remove(ingr);
        }

        public bool CanAddIngredientToMeal(Meal meal, Ingredient ingr)
        {
            if (CanOperateMeal(meal) == false)
                return false;

            if (IngredientController.Instance.CanOperateIngredient(ingr) == false)
                return false; ;

            if (meal.Ingredients.Contains(ingr))
                return false;

            return true;
        }

        public void AddIngredientToMeal(Meal meal, Ingredient ingredient)
        {
            if (CanOperateMeal(meal) == false)
                throw new ArgumentException($"Meal with name {meal.Name} doesn't exists in database");

            if (IngredientController.Instance.CanOperateIngredient(ingredient) == false)
                throw new ArgumentException($"Ingredient with name {ingredient.Name} doesn't exists in database");

            if (meal.Ingredients.Contains(ingredient))
                throw new ArgumentException($"Meal {meal.Name} also contains {ingredient.Name}");

            meal.Ingredients.Add(ingredient);
        }

        public void RemoveIngredientFromMeal(Meal meal, Ingredient ingredient)
        {
            if (CanOperateMeal(meal) == false)
                throw new ArgumentException($"Meal with name {meal.Name} doesn't exists in database");

            if (IngredientController.Instance.CanOperateIngredient(ingredient) == false)
                throw new ArgumentException($"Ingredient with name {ingredient.Name} doesn't exists in database");

            if (meal.Ingredients.Contains(ingredient) == false)
                throw new ArgumentException($"Meal {meal.Name} do not contains {ingredient.Name}");

            meal.Ingredients.Remove(ingredient);
        }

        public bool CanOperateMeal(Meal meal)
            => Meals.Contains(meal);
    }
}
