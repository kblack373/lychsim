
using System;
//using ComUn;

namespace Lychgate
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            ComUn.CreateUnit();
            Console.ReadLine(); // keeps console open
        }
        //Console.ReadLine(); 
    }
}
// todo:
// 1. implement each <object>.py class into C#. Remember that engine.py is the python driver.
// See https://aka.ms/new-console-template for more information
// 2. invoke the engine class from this class '