using BuisnessLogic;
using BusinessLogic;
using PresentationLayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyApp // Note: actual namespace depends on the project name.
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RootController.Instance.Initialize();

            var input = string.Empty;
            while (input != "q")
            {
                Console.WriteLine("Hi!\n" +
                    "U are at the main page\n" +
                    "Choose option:\n" +
                    "1. Change order\n" +
                    "2. Edit menu\n" +
                    "3. Edit meal\n" +
                    "q for Quit");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        {
                            (new ChangeOrdersCommand()).Execute();
                            break;
                        }
                    case "2":
                        {
                            (new OperateMenuCommand()).Execute();  
                            break;
                        }
                    case "3":
                        {
                            (new OperateMealsCommand()).Execute();
                            break;
                        }
                }
            }

            RootController.Instance.Quit();
        }
    }
}