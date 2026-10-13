using System;

namespace CrazyPC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Call the multithreaded simulation method
            CrazyPC.CrazyFunctionCall();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
