using BuisnessLogic;

namespace PresentationLayer.Commands
{
    internal class SelectOrderForEditingCommand : Command
    {
        public override void Execute()
        {
            try
            {
                for (int i = 0; i < OrderController.Instance.Orders.Count; i++)
                {
                    var order = OrderController.Instance.Orders[i];
                    Console.WriteLine($"{i + 1}) Order #{order.OrderNumber}");
                }

                var selectedIndex = GetSelectedIndex();
                var selectedOrder = OrderController.Instance.Orders[selectedIndex];

                (new OperateSelectedOrder()).Execute(selectedOrder);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private int GetSelectedIndex()
        {
            var selectedCorrectly = false;
            var orderIndex = -1;
            while (!selectedCorrectly)
            {
                Console.Write("Select order number: ");
                var indexStr = Console.ReadLine();
                selectedCorrectly = int.TryParse(indexStr, out orderIndex);
                if (selectedCorrectly == false)
                {
                    Console.WriteLine("Unable to parse value, try one more time");
                    continue;
                }

                orderIndex--;
                selectedCorrectly = orderIndex >= 0 && orderIndex < OrderController.Instance.Orders.Count;
                if (selectedCorrectly == false)
                {
                    Console.WriteLine("Index is out if ranges");
                    continue;
                }

                selectedCorrectly = true;
            }

            return orderIndex;
        }
    }
}
