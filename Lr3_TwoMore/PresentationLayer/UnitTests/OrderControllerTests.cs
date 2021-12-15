using BuisnessLogic;
using DataLayer;
using NUnit.Framework;
using System;
using System.Linq;

namespace UnitTests
{
    internal class OrderControllerTests
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
        public void NewOrder_NotNull()
        {
            var newOrder = OrderController.Instance.StartNewOrder();
            Assert.NotNull(newOrder);
        }

        [Test]
        public void CanAddMealToOrder_UnknownOrder()
        {
            var newOrder = new OrderRecord();
            var meal = new Meal() { Name = "testMeal" };
            var amountToAdd = -5;

            var canAdd = OrderController.Instance.CanAddMealToOrder(newOrder, meal, amountToAdd);

            Assert.False(canAdd);
        }

        [Test]
        public void CanAddMealToOrder_UnknownMeal()
        {
            var newOrder = OrderController.Instance.StartNewOrder();
            var meal = new Meal() { Name = "testMeal" };
            var amountToAdd = -5;

            var canAdd = OrderController.Instance.CanAddMealToOrder(newOrder, meal, amountToAdd);

            Assert.False(canAdd);
        }

        [Test]
        public void CanAddMealToOrder_LessZeroAmount()
        {
            var newOrder = OrderController.Instance.StartNewOrder();
            var meal = MealController.Instance.Meals.First();
            var amountToAdd = -5;

            var canAdd = OrderController.Instance.CanAddMealToOrder(newOrder, meal, amountToAdd);

            Assert.False(canAdd);
        }

        [Test]
        public void CanAddMealToOrder_Successful()
        {
            var newOrder = OrderController.Instance.StartNewOrder();
            var meal = MealController.Instance.Meals.First();
            var amountToAdd = 3;

            var canAdd = OrderController.Instance.CanAddMealToOrder(newOrder, meal, amountToAdd);

            Assert.True(canAdd);
        }

        [Test]
        public void AddMealToOrder_Successful()
        {
            var newOrder = OrderController.Instance.StartNewOrder();
            var meal = MealController.Instance.Meals.First();
            var amountToAdd = 3;

            OrderController.Instance.AddMealToOrder(newOrder, meal, amountToAdd);

            Assert.Contains(newOrder, OrderController.Instance.Orders);
        }

        [Test]
        public void AddMealToOrder_Exception()
        {
            var newOrder = OrderController.Instance.StartNewOrder();
            var meal = MealController.Instance.Meals.First();
            var amountToAdd = -3;

            var ex = Assert.Throws<ArgumentException>(() => OrderController.Instance.AddMealToOrder(newOrder, meal, amountToAdd));
            Assert.AreEqual($"Can't add meal {meal.Name} in amount {amountToAdd} to order #{newOrder.OrderNumber}", ex.Message);
        }

        [Test]
        public void CanRemoveMealToOrder_UnknownOrder()
        {
            var newOrder = new OrderRecord();
            var meal = new Meal() { Name = "testMeal" };
            var amountToAdd = -5;

            var canAdd = OrderController.Instance.CanRemoveMealFromOrder(newOrder, meal, amountToAdd);

            Assert.False(canAdd);
        }

        [Test]
        public void CanRemoveMealToOrder_UnknownMeal()
        {
            var newOrder = OrderController.Instance.StartNewOrder();
            var meal = new Meal() { Name = "testMeal" };
            var amountToAdd = -5;

            var canAdd = OrderController.Instance.CanRemoveMealFromOrder(newOrder, meal, amountToAdd);

            Assert.False(canAdd);
        }

        [Test]
        public void CanRemoveMealToOrder_LessZeroAmount()
        {
            var newOrder = OrderController.Instance.StartNewOrder();
            var meal = MealController.Instance.Meals.First();
            var amountToAdd = -5;

            var canAdd = OrderController.Instance.CanRemoveMealFromOrder(newOrder, meal, amountToAdd);

            Assert.False(canAdd);
        }

        [Test]
        public void CanRemoveMealToOrder_OrderDoesntContainsMeal()
        {
            var newOrder = OrderController.Instance.StartNewOrder();
            var meal = MealController.Instance.Meals.First();
            var amountToAdd = 5;

            var canAdd = OrderController.Instance.CanRemoveMealFromOrder(newOrder, meal, amountToAdd);

            Assert.False(canAdd);
        }

        [Test]
        public void CanRemoveMealToOrder_AmountLessThanOrdered()
        {
            var newOrder = OrderController.Instance.StartNewOrder();
            var meal = MealController.Instance.Meals.First();
            var amountToAdd = 5;
            OrderController.Instance.AddMealToOrder(newOrder, meal, amountToAdd);

            var amountToRemove = 8;
            var canAdd = OrderController.Instance.CanRemoveMealFromOrder(newOrder, meal, amountToRemove);

            Assert.False(canAdd);
        }

        [Test]
        public void CanRemoveMealToOrder_Successful()
        {
            var newOrder = OrderController.Instance.StartNewOrder();
            var meal = MealController.Instance.Meals.First();
            var amountToAdd = 5;
            OrderController.Instance.AddMealToOrder(newOrder, meal, amountToAdd);

            var amountToRemove = 3;
            var canAdd = OrderController.Instance.CanRemoveMealFromOrder(newOrder, meal, amountToRemove);

            Assert.True(canAdd);
        }

        [Test]
        public void RemoveMealToOrder_Successful_Removed()
        {
            var newOrder = OrderController.Instance.StartNewOrder();
            var meal = MealController.Instance.Meals.First();
            var amountToAdd = 5;
            OrderController.Instance.AddMealToOrder(newOrder, meal, amountToAdd);

            OrderController.Instance.RemoveMealFromOrder(newOrder, meal, amountToAdd);
            var isRemoved = newOrder.Meals.Any(m => m.Meal == meal) == false;

            Assert.True(isRemoved);
        }

        [Test]
        public void RemoveMealToOrder_Successful_AmountDecreased()
        {
            var newOrder = OrderController.Instance.StartNewOrder();
            var meal = MealController.Instance.Meals.First();
            var amountToAdd = 5;
            OrderController.Instance.AddMealToOrder(newOrder, meal, amountToAdd);
            var amountToRemove = 2;

            OrderController.Instance.RemoveMealFromOrder(newOrder, meal, amountToRemove);
            var isRemoved = newOrder.Meals.Any(m => m.Meal == meal) == false;

            Assert.False(isRemoved);
            var mealRecord = newOrder.Meals.First(m => m.Meal == meal);
            Assert.AreEqual(mealRecord.Count, 3);
        }


        [Test]
        public void RemoveMealToOrder_Exception()
        {
            var newOrder = OrderController.Instance.StartNewOrder();
            var meal = MealController.Instance.Meals.First();
            var amountToAdd = -3;

            var ex = Assert.Throws<ArgumentException>(() => OrderController.Instance.RemoveMealFromOrder(newOrder, meal, amountToAdd));
            Assert.AreEqual($"Can't remove meal{meal.Name} in amount {amountToAdd} from order #{newOrder.OrderNumber}", ex.Message);
        }
    }
}
