using BuisnessLogic;
using DataLayer;
using NUnit.Framework;
using System;
using System.Linq;

namespace UnitTests
{
    public class MenuControllerTests
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
        public void CanAddMenu_CorrectName()
        {
            var mealName = "testMeal";

            var canAdd = MenuController.Instance.CanAddMenu(mealName);

            Assert.True(canAdd);
        }

        [Test]
        public void CanAddMenu_IncorrectName_EmptyName()
        {
            var mealName = string.Empty;

            var canAdd = MenuController.Instance.CanAddMenu(mealName);

            Assert.False(canAdd);
        }

        [Test]
        public void CanAddMenu_IncorrectName_ContainsName()
        {
            var menuName = "menu1";

            var canAdd = MenuController.Instance.CanAddMenu(menuName);

            Assert.False(canAdd);
        }

        [Test]
        public void AddMenu_CorrectName()
        {
            var mealName = "testMenu";

            MenuController.Instance.AddMenu(mealName);

            var createdMeal = MenuController.Instance.Menus.FirstOrDefault(meal => meal.Name == mealName);
            Assert.IsNotNull(createdMeal);
        }

        [Test]
        public void AddMenu_IncorrectName_Exception()
        {
            var menuName = "menu1";

            var ex = Assert.Throws<ArgumentException>(() => MenuController.Instance.AddMenu(menuName));
            Assert.AreEqual($"Can't add menu with name {menuName}", ex.Message);
        }

        [Test]
        public void RemoveMenu_Successful()
        {
            var newMeal = MenuController.Instance.Menus.First();
            MenuController.Instance.RemoveMenu(newMeal);
            var isStillContains = MenuController.Instance.Menus.Contains(newMeal);
            Assert.False(isStillContains);
        }

        [Test]
        public void RemoveMenu_NotExisting_Exception()
        {
            var newMeal = new Menu()
            {
                Name = "testName",
            };

            var ex = Assert.Throws<ArgumentException>(() => MenuController.Instance.RemoveMenu(newMeal));
            Assert.AreEqual($"Can't remove menu {newMeal.Name}", ex.Message);
        }

        [Test]
        public void CanAddMeal_MenuNotExisting()
        {
            var newMenu = new Menu()
            {
                Name = "testName",
            };

            var meal = new Meal()
            {
                Name = "testIngr",
            };

            var canAdd = MenuController.Instance.CanAddMealToMenu(newMenu, meal);

            Assert.False(canAdd);
        }

        [Test]
        public void CanAddMeal_MealNotExisting()
        {
            var menu = MenuController.Instance.Menus.First();

            var meal = new Meal()
            {
                Name = "testIngr",
            };

            var canAdd = MenuController.Instance.CanAddMealToMenu(menu, meal);

            Assert.False(canAdd);
        }

        [Test]
        public void CanAddIngredient_MenuDoesntContainMeal()
        {
            var newMeal = MenuController.Instance.Menus.First();

            var ingr = MealController.Instance.Meals.Last();

            var canAdd = MenuController.Instance.CanAddMealToMenu(newMeal, ingr);

            Assert.True(canAdd);
        }

        [Test]
        public void AddIngredient_Successful()
        {
            var newMeal = MenuController.Instance.Menus.First();

            var ingr = MealController.Instance.Meals.Last();

            MenuController.Instance.AddMealToMenu(newMeal, ingr);

            Assert.Contains(ingr, newMeal.Meals);
        }

        [Test]
        public void CanRemoveIngredient_MealNotExisting()
        {
            var newMeal = new Menu()
            {
                Name = "testName",
            };

            var ingr = new Meal()
            {
                Name = "testIngr",
            };

            var canRemove = MenuController.Instance.CanRemoveMealFromMenu(newMeal, ingr);

            Assert.False(canRemove);
            }

        [Test]
        public void CanRemoveIngredient_IngrNotExisting()
        {
            var newMeal = MenuController.Instance.Menus.First();

            var ingr = new Meal()
            {
                Name = "testIngr",
            };

            var canRemove = MenuController.Instance.CanRemoveMealFromMenu(newMeal, ingr);

            Assert.False(canRemove);
        }

        [Test]
        public void CanRemoveIngredient_MealContainsIngs()
        {
            var newMeal = MenuController.Instance.Menus.First();

            var ingr = MealController.Instance.Meals.Last();

            var canRemove = MenuController.Instance.CanRemoveMealFromMenu(newMeal, ingr);

            Assert.False(canRemove);
        }

        [Test]
        public void RemoveIngredient_Exception()
        {
            var newMeal = MenuController.Instance.Menus.First();

            var ingr = MealController.Instance.Meals.Last();

            var ex = Assert.Throws<ArgumentException>(() => MenuController.Instance.RemoveMealFromMenu(newMeal, ingr));
            Assert.AreEqual($"Can't remove meal {ingr.Name} from menu {newMeal.Name}", ex.Message);
        }
    }
}