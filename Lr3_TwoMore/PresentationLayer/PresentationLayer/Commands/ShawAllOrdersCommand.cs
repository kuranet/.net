using BuisnessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Commands
{
    public class ShawAllOrdersCommand : Command
    {
        public override void Execute()
        {
            try
            {
                var allOrders = OrderController.Instance.Orders;

                if (allOrders.Any() == false)
                {
                    Console.WriteLine("Any order isn't created");
                    return;
                }

                for (int i = 0; i < allOrders.Count; i++)
                {
                    var order = allOrders[i];

                    Console.Write($"{i + 1}) "); EntityVisualizer.Print(order);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
