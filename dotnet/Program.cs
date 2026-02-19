
namespace Lychgate
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            //ComUn.CreateUnit();
            //Console.ReadLine(); // keeps console open

            //LychEngine eng = new(@"C:\Users\kenda\OneDrive\Documents\development\lychgate\simulation\config\default.xml");
            
            string filePath = @"C:\Users\kenda\OneDrive\Documents\development\lychgate\simulation\config\default.json";
            LychEngine eng = new(filePath);
            eng.RunSim();
        }
        //Console.ReadLine(); 
    }
}
// todo:
// 1. implement each <object>.py class into C#. Remember that engine.py is the python driver.
// See https://aka.ms/new-console-template for more information
// 2. invoke the engine class from this class '