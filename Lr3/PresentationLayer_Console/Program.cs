using System;
using BusinessLogic;
using DataAccess;

namespace PresentationLayer_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var rootController = new RootController();
            rootController.Initialize();
            //    var sd = new PostITEntities();
            //var nebuController = new MenuController();
            Console.WriteLine("Hello World!");
        }
    }
}
