using BuisnessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public static class EntityVisualizer
    {
        public static void Print(OrderRecord order)
        {
            Console.WriteLine($"order #{order.OrderNumber}");

            if (order.Meals.Any() == false)
            {
                Console.WriteLine("\tAny meal isn't added");
                return;
            }

            for (int j = 0; j < order.Meals.Count; j++)
            {
                var meal = order.Meals[j];
                Console.WriteLine($"\t{j + 1}) Meal {meal.Meal.Name} x{meal.Count}");
            }

            Console.WriteLine($"\t\t Total price: {order.Price}");
        }
    }
}
