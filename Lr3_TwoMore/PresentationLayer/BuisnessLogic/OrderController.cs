using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic
{
    public class OrderController
    {
        private static OrderController instance;

        private OrderController()
        { }

        public static OrderController Instance
        {
            get
            {
                if (instance == null)
                    instance = new OrderController();
                return instance;
            }
        }

        private List<OrderRecord> _orders = new List<OrderRecord>();
        public List<OrderRecord> Orders => _orders;

        public OrderRecord StartNewOrder()
        {
            var maxNumber = _orders.Any() ? _orders.Max(order => order.OrderNumber) : 0;
            var newOrder = new OrderRecord() { OrderNumber = ++maxNumber, };
            _orders.Add(newOrder);
            return newOrder;
        }

        public void AddMealToOrder(OrderRecord order, Meal mealToAdd, int amountToAdd)
        {
            if (CanOperateOrder(order) == false)
                throw new ArgumentException($"Order {order.OrderNumber} doesn't exists in database");

            if (amountToAdd <= 0)
                throw new ArgumentException("Tring to add negative meal amount");

            var mealRecord = order.Meals.FirstOrDefault(meal => meal.Meal == mealToAdd);

            if (mealRecord == null)
            {
                mealRecord = new MealRecord { Meal = mealToAdd, Count = 0 };
                order.Meals.Add(mealRecord);
            }

            mealRecord.Count += amountToAdd;
        }

        public void RemoveMealFromOrder(OrderRecord order, Meal mealToRemove, int amountToRemove)
        {
            if (CanOperateOrder(order) == false)
                throw new ArgumentException($"Order {order.OrderNumber} doesn't exists in database");

            if (amountToRemove <= 0)
                throw new ArgumentException("Tring to remove negative meal amount");

            var mealRecord = order.Meals.FirstOrDefault(meal => meal.Meal == mealToRemove);

            if (mealRecord.Equals(default))
                throw new NullReferenceException($"Order {order.OrderNumber} doesn't contains {mealToRemove.Name} meal");

            if (mealRecord.Count < amountToRemove)
                throw new ArgumentException($"In order {order.OrderNumber} meal with {mealToRemove.Name} has less ordered amount");

            mealRecord.Count -= amountToRemove;

            if (mealRecord.Count != 0)
                return;

            order.Meals.Remove(mealRecord);
        }

        private bool CanOperateOrder(OrderRecord order)
        => _orders.Contains(order);
    }
}
