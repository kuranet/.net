using System;
using System.Collections.Generic;
using Lr1_ClassLibrary;

namespace Lr1_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var intStack = new MyStack<int>();
            intStack.SuccesfullAddition 
                += (source, el) => Print(el.Message);
            intStack.SuccesfullClearing 
                += (source, el) => Print($"{source}: cleared sucessfully");

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
            Print();

            Print($"Check contains 6: {intStack.Contains(6)}");
            Print($"Check contains 2: {intStack.Contains(2)}");
            Print();

            intStack.Clear();
            Print("Test enumerable");
            foreach (var element in intStack)
            {
                Print(element.ToString());
            }
            Print();

        }

        static void Print(string mesage = default)
        {
            Console.WriteLine(mesage);
        }
    }
}
