using BuisnessLogic;

namespace PresentationLayer.Commands
{
    internal class OperateSelectedOrder : Command<OrderRecord>
    {
        public override void Execute(OrderRecord selectedOrder)
        {
            Console.WriteLine();
            Console.WriteLine($"You selected "); EntityVisualizer.Print(selectedOrder);



            Console.WriteLine("Options:\n" +
                "1. Add meal");

            var canRemove = selectedOrder.Meals.Any();
            if (canRemove)
                Console.WriteLine("2. Remove meal");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    {

                        var mealIndex = SelectMealToAdd();
                        var meal = MealController.Instance.Meals[mealIndex];

                        var selectedAmount = 0;
                        var isSelectedCorrectly = false;
                        while (isSelectedCorrectly == false)
                        {
                            Console.Write($"Select how many to remove (>0): ");
                            var countStr = Console.ReadLine();
                            isSelectedCorrectly = int.TryParse(countStr, out selectedAmount);
                            if (isSelectedCorrectly == false)
                                continue;

                            isSelectedCorrectly = selectedAmount > 0;
                        }
                        
                        OrderController.Instance.AddMealToOrder(selectedOrder, meal, selectedAmount);
                        break;
                    }
                case "2":
                    {
                        if (canRemove == false)
                            return;

                        var mealIndex = SelectMealToRemove(selectedOrder);
                        var meal = selectedOrder.Meals[mealIndex];

                        var selectedAmount = 0;
                        var isSelectedCorrectly = false;
                        while (isSelectedCorrectly == false)
                        {
                            Console.Write($"Select how many to remove (1-{meal.Count}): ");
                            var countStr = Console.ReadLine();
                            isSelectedCorrectly = int.TryParse(countStr, out selectedAmount);
                            if (isSelectedCorrectly == false)
                                continue;

                            isSelectedCorrectly = selectedAmount > 0 && selectedAmount <= meal.Count;
                        }

                        OrderController.Instance.RemoveMealFromOrder(selectedOrder, meal.Meal, selectedAmount);
                        break;
                    }
            }
        }

        private int SelectMealToAdd()
        {
            for (int i = 0; i < MealController.Instance.Meals.Count; i++)
            {
                var meal = MealController.Instance.Meals[i];
                Console.WriteLine($"{i + 1}) {meal.Name}");
            }

            var isSelectedCorrectly = false;
            var selectedIndex = -1;
            while (isSelectedCorrectly == false)
            {
                Console.Write("Select meal number: ");
                var indexStr = Console.ReadLine();
                isSelectedCorrectly = int.TryParse(indexStr, out selectedIndex);
                if (isSelectedCorrectly == false)
                {
                    Console.WriteLine("Unable to parse value, try one more time");
                    continue;
                }

                selectedIndex--;
                isSelectedCorrectly = selectedIndex >= 0 && selectedIndex < MealController.Instance.Meals.Count;
                if (isSelectedCorrectly == false)
                {
                    Console.WriteLine("Index is out if ranges");
                    continue;
                }

                isSelectedCorrectly = true;
            }

            return selectedIndex;

        }

        private int SelectMealToRemove(OrderRecord selectedOrder)
        {
            for (int i = 0; i < selectedOrder.Meals.Count; i++)
            {
                var meal = selectedOrder.Meals[i];
                Console.WriteLine($"{i + 1}) {meal.Meal.Name} x{meal.Count}");
            }

            var isSelectedCorrectly = false;
            var selectedIndex = -1;
            while (isSelectedCorrectly == false)
            {
                Console.Write("Select meal number: ");
                var indexStr = Console.ReadLine();
                isSelectedCorrectly = int.TryParse(indexStr, out selectedIndex);
                if (isSelectedCorrectly == false)
                {
                    Console.WriteLine("Unable to parse value, try one more time");
                    continue;
                }

                selectedIndex--;
                isSelectedCorrectly = selectedIndex >= 0 && selectedIndex < selectedOrder.Meals.Count;
                if (isSelectedCorrectly == false)
                {
                    Console.WriteLine("Index is out if ranges");
                    continue;
                }

                isSelectedCorrectly = true;
            }

            return selectedIndex;
        }
    }
}
