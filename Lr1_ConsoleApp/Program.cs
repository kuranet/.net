using System;
using Lr1_ClassLibrary;

namespace Lr1_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var intStack = new MyStack<int>();
            for (var i = 0; i < 5; i++)
            {
                intStack.Push(i + 1);
            }

            Print($"Get stack peek: {intStack.Peek()}");
            Print();

            Print($"Poped element: {intStack.Pop()}");
            Print($"Get new stack peek: {intStack.Peek()}");
            Print();


            Print("Test enumerable");
            foreach(var element in intStack)
            {
                Print(element.ToString());
            }
        }

        static void Print(string mesage = default)
        {
            Console.WriteLine(mesage);
        }
    }
}
