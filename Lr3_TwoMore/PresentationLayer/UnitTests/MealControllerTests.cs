using BuisnessLogic;
using DataLayer;
using NUnit.Framework;
using System;
using System.Linq;

namespace UnitTests
{
    public class MealControllerTests
    {
        [SetUp]
        public void Setup()
        {
            var database = new DataProviderMock();

            IngredientController.Instance.Initialize(database);
            MenuController.Instance.Initialize(database);
            MealController.Instance.Initialize(database);
        }

        [Test]
        public void CanAddMeal_CorrectName()
        {
            var mealName = "testMeal";

            var canAdd = MealController.Instance.CanAddMeal(mealName);

            Assert.True(canAdd);
        }

        [Test]
        public void CanAddMeal_IncorrectName_EmptyName()
        {
            var mealName = string.Empty;

            var canAdd = MealController.Instance.CanAddMeal(mealName);

            Assert.False(canAdd);
        }

        [Test]
        public void CanAddMeal_IncorrectName_ContainsName()
        {
            var mealName = "meal1";

            var canAdd = MealController.Instance.CanAddMeal(mealName);

            Assert.False(canAdd);
        }

        [Test]
        public void AddMeal_CorrectName()
        {
            var mealName = "testMeal";

            MealController.Instance.AddMeal(mealName);

            var createdMeal = MealController.Instance.Meals.FirstOrDefault(meal => meal.Name == mealName);
            Assert.IsNotNull(createdMeal);
        }

        [Test]
        public void AddMeal_IncorrectName_Exception()
        {
            var mealName = "meal1";

            var ex = Assert.Throws<ArgumentException>(() => MealController.Instance.AddMeal(mealName));
            Assert.AreEqual($"Can't add meal with name {mealName}", ex.Message);
        }

        [Test]
        public void RemoveMeal_Successful()
        {
            var newMeal = MealController.Instance.Meals.First();
            MealController.Instance.RemoveMeal(newMeal);
            var isStillContains = MealController.Instance.Meals.Contains(newMeal);
            Assert.False(isStillContains);
        }

        [Test]
        public void RemoveMeal_NotExisting_Exception()
        {
            var newMeal = new Meal()
            {
                Name = "testName",
            };

            var ex = Assert.Throws<ArgumentException>(() => MealController.Instance.RemoveMeal(newMeal));
            Assert.AreEqual($"Can't remove meal {newMeal.Name}", ex.Message);
        }

        [Test]
        public void CanAddIngredient_MealNotExisting()
        {
            var newMeal = new Meal()
            {
                Name = "testName",
            };

            var ingr = new Ingredient()
            {
                Name = "testIngr",
            };

            var canAdd = MealController.Instance.CanAddIngredientToMeal(newMeal, ingr);

            Assert.False(canAdd);
        }

        [Test]
        public void CanAddIngredient_IngrNotExisting()
        {
            var newMeal = MealController.Instance.Meals.First();

            var ingr = new Ingredient()
            {
                Name = "testIngr",
            };

            var canAdd = MealController.Instance.CanAddIngredientToMeal(newMeal, ingr);

            Assert.False(canAdd);
        }

        [Test]
        public void CanAddIngredient_MealDoesntContainsIngs()
        {
            var newMeal = MealController.Instance.Meals.First();

            var ingr = IngredientController.Instance.Ingredients.Last();

            var canAdd = MealController.Instance.CanAddIngredientToMeal(newMeal, ingr);

            Assert.True(canAdd);
        }

        [Test]
        public void AddIngredient_Successful()
        {
            var newMeal = MealController.Instance.Meals.First();

            var ingr = IngredientController.Instance.Ingredients.Last();

            MealController.Instance.AddIngredientToMeal(newMeal, ingr);

            Assert.Contains(ingr, newMeal.Ingredients);
        }

        [Test]
        public void CanRemoveIngredient_MealNotExisting()
        {
            var newMeal = new Meal()
            {
                Name = "testName",
            };

            var ingr = new Ingredient()
            {
                Name = "testIngr",
            };

            var canRemove = MealController.Instance.CanRemoveIngredientFromMeal(newMeal, ingr);

            Assert.False(canRemove);
        }

        [Test]
        public void CanRemoveIngredient_IngrNotExisting()
        {
            var newMeal = MealController.Instance.Meals.First();

            var ingr = new Ingredient()
            {
                Name = "testIngr",
            };

            var canRemove = MealController.Instance.CanRemoveIngredientFromMeal(newMeal, ingr);

            Assert.False(canRemove);
        }

        [Test]
        public void CanRemoveIngredient_MealContainsIngs()
        {
            var newMeal = MealController.Instance.Meals.First();

            var ingr = IngredientController.Instance.Ingredients.Last();

            var canRemove = MealController.Instance.CanRemoveIngredientFromMeal(newMeal, ingr);

            Assert.False(canRemove);
        }

        [Test]
        public void RemoveIngredient_Exception()
        {
            var newMeal = MealController.Instance.Meals.First();

            var ingr = IngredientController.Instance.Ingredients.Last();

            var ex = Assert.Throws<ArgumentException>(() => MealController.Instance.RemoveIngredientFromMeal(newMeal, ingr));
            Assert.AreEqual($"Can't remove ingredient {ingr.Name} from meal {newMeal.Name}", ex.Message);
        }
    }
}