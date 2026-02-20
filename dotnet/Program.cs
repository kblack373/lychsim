
namespace Lychgate
{
    class Program
    {

        private const string defaultFilePath = @".\defaultconfig.json";
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            //ComUn.CreateUnit();
            //Console.ReadLine(); // keeps console open

            //LychEngine eng = new(@"C:\Users\kenda\OneDrive\Documents\development\lychgate\simulation\config\default.xml");
            //= @"C:\Users\kenda\OneDrive\Documents\development\lychgate\simulation\config\default.json";

            string filePath = "";
            Console.WriteLine("LychGate Simulator v0.2");
            Console.WriteLine("Enter absolute filepath to config.json (blank for default)> ");
            filePath = Console.ReadLine().ToString();
            if (filePath == "")
            {

                filePath = defaultFilePath;
            }
            if (System.IO.File.Exists(filePath))
            {
                LychEngine eng = new(filePath);
                eng.RunSim();
            } else
            {

                Console.WriteLine("Cannot read file.");
            }
            
        }
        //Console.ReadLine(); 
    }
}
// todo:
// 1. implement each <object>.py class into C#. Remember that engine.py is the python driver.
// See https://aka.ms/new-console-template for more information
// 2. invoke the engine class from this class '