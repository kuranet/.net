using DataLayer;

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

        public bool CanAddMealToOrder(OrderRecord order, Meal mealToAdd, int amountToAdd)
        {
            // Unknown order.
            if (CanOperateOrder(order) == false)
                return false;

            // Unknown meal.
            if (MealController.Instance.CanOperateMeal(mealToAdd) == false)
                return false;

            if (amountToAdd <= 0)
                return false;

            return true;
        }

        public void AddMealToOrder(OrderRecord order, Meal mealToAdd, int amountToAdd)
        {
            if (CanAddMealToOrder(order, mealToAdd, amountToAdd) == false)
                throw new ArgumentException($"Can't add meal {mealToAdd.Name} in amount {amountToAdd} to order #{order.OrderNumber}");

            var mealRecord = order.Meals.FirstOrDefault(meal => meal.Meal == mealToAdd);

            if (mealRecord == null)
            {
                mealRecord = new MealRecord { Meal = mealToAdd, Count = 0 };
                order.Meals.Add(mealRecord);
            }

            mealRecord.Count += amountToAdd;
        }

        public bool CanRemoveMealFromOrder(OrderRecord order, Meal mealToRemove, int amountToRemove)
        {
            // Unknown order.
            if (CanOperateOrder(order) == false)
                return false;

            // Unknown meal.
            if (MealController.Instance.CanOperateMeal(mealToRemove) == false)
                return false;

            // Amount less than zero.
            if (amountToRemove <= 0)
                return false;

            var mealRecord = order.Meals.FirstOrDefault(meal => meal.Meal == mealToRemove);

            // Don't have seal in record.
            if (mealRecord == null)
                return false;

            // Can't remove lass than amount.
            if (mealRecord.Count < amountToRemove)
                return false;

            return true;
        }

        public void RemoveMealFromOrder(OrderRecord order, Meal mealToRemove, int amountToRemove)
        {
            if (CanRemoveMealFromOrder(order, mealToRemove, amountToRemove) == false)
                throw new ArgumentException($"Can't remove meal{mealToRemove.Name} in amount {amountToRemove} from order #{order.OrderNumber}");
            
            var mealRecord = order.Meals.First(meal => meal.Meal == mealToRemove);
            mealRecord.Count -= amountToRemove;

            if (mealRecord.Count != 0)
                return;

            order.Meals.Remove(mealRecord);
        }

        private bool CanOperateOrder(OrderRecord order)
        => _orders.Contains(order);
    }
}
