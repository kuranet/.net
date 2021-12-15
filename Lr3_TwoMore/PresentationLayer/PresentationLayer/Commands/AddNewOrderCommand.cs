using BuisnessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Commands
{
    internal class AddNewOrderCommand : Command
    {
        public override void Execute()
        {
            try
            {
                var newOrder = OrderController.Instance.StartNewOrder();
                Console.WriteLine($"Order #{newOrder.OrderNumber} successfully created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}
