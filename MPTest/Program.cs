using System;
using NumericMethods.Objects;
using System.Diagnostics;

namespace MPTest
{
    class Program
    {
        static void Main(string[] args) {
            var system1 = new LSystem(1 ,1, 50);
            system1.GenerateRegularSystem();
            var system2 = new LSystem(5, 5, 50);
            system2.GenerateRegularSystem();

            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
            system1.Matrix.GetInvertibleMatrix();
            sw1.Stop();
            Console.WriteLine((sw1.ElapsedMilliseconds / 1000.0).ToString());

            Matrix.SetNumberOfThreads(4);
            
            Stopwatch sw2 = new Stopwatch();
            sw2.Start();
            system1.Matrix.GetInvertibleMatrix();
            sw2.Stop();
            Console.WriteLine((sw2.ElapsedMilliseconds / 1000.0).ToString());

            Console.Read();
        }
    }
}
