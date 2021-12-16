using BuisnessLogic;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication5.Models;

namespace WebApplication2.Controllers
{
    public class OrderEditingController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Orders = OrderController.Instance.Orders;
            return View();
        }

        [HttpGet]
        [Route("OrderEditing/Add")]
        public ActionResult AddOrder()
        {
            OrderController.Instance.StartNewOrder();

            return Redirect("/OrderEditing/Index");
        }

        [HttpGet]
        [Route("OrderEditing/Edit/{orderNumber}")]
        public ActionResult Edit(int orderNumber)
        {
            ViewBag.SeletedOrderNumber = orderNumber;
           
            var order = OrderController.Instance.Orders.FirstOrDefault(o => o.OrderNumber == orderNumber);
            
            var mealsInOrder = new List<WebApplication5.Models.MealRecord>();
            foreach (var item in order.Meals)
            {
                var newRecord = new WebApplication5.Models.MealRecord()
                {
                    Meal = item.Meal,
                    AddedAmount = item.Count,
                };
                mealsInOrder.Add(newRecord);
            }
            ViewBag.InOrderMeals = mealsInOrder;

            var mealsCanBeAdded = new List<WebApplication5.Models.MealRecord>();
            var allMeals = MealController.Instance.Meals;
            foreach (var item in allMeals)
            {
                var newRecord = new WebApplication5.Models.MealRecord()
                {
                    Meal = item,
                    AddedAmount = 0
                };
                mealsCanBeAdded.Add(newRecord);
            }
            ViewBag.MealsForAdding = mealsCanBeAdded;

            return View();
        }

        [HttpGet]
        [Route("OrderEditing/AddToMeal/{orderId}&{id}")]
        public ActionResult AddToMeal(int orderId, int id)
        {
            var order = OrderController.Instance.Orders.FirstOrDefault(o => o.OrderNumber == orderId);
            var mealToAdd = MealController.Instance.Meals.FirstOrDefault(m => m.Id == id);
            OrderController.Instance.AddMealToOrder(order, mealToAdd, 1);
            
            return Redirect($"/OrderEditing/Edit/{orderId}");
        }

        [HttpGet]
        [Route("OrderEditing/RemoveMeal/{orderId}&{id}")]
        public ActionResult RemoveMeal(int orderId, int id)
        {
            var order = OrderController.Instance.Orders.FirstOrDefault(o => o.OrderNumber == orderId);
            var mealToAdd = MealController.Instance.Meals.FirstOrDefault(m => m.Id == id);
            OrderController.Instance.RemoveMealFromOrder(order, mealToAdd, 1);

            return Redirect($"/OrderEditing/Edit/{orderId}");
        }
    }
}