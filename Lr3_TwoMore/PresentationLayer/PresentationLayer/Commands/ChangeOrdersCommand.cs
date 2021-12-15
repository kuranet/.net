namespace PresentationLayer.Commands
{
    internal class ChangeOrdersCommand : Command
    {
        public override void Execute()
        {
            var input = string.Empty;
            while (input != "b")
            {
                Console.WriteLine("Order operations\n" +
                                    "Choose option:\n" +
                                    "1. See all orders\n" +
                                    "2. Add new order\n" +
                                    "3. Select order\n" +
                                    "b for go back");

                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        {
                            (new ShawAllOrdersCommand()).Execute();
                            break;
                        }
                    case "2":
                        {
                            (new AddNewOrderCommand()).Execute();
                            break;
                        }
                    case "3":
                        {
                            (new SelectOrderForEditingCommand()).Execute();
                            break;
                        }

                    case "b":
                        return;

                    default:
                        {
                            Console.WriteLine("Wrong input");
                            break;
                        }
                }

                Console.WriteLine();
            }
        }
    }
}
