using NumericMethods.Methods;
using NumericMethods.Objects;
using System;

namespace Lab4
{
    class Program
    {
        private const int STUDENT_NUMBER = 9;
        private const int GROUP_NUMBER = 1;

        static void Main()
        {
            Console.WriteLine("Lab4: Tridiagonal algorithm       \n-----------------------------------");
            try
            {
                var system = new LSystem(STUDENT_NUMBER, GROUP_NUMBER, 4);
                system.GenerateTridiagonalSystem();

                Console.WriteLine("Our system:");
                system.Print();

                var solution = Tridiagonal.Calculate(system);
                Console.WriteLine("Solutions via Tridiagonal algorithm:");
                solution.Print();

                Console.WriteLine("Residuals: ");
                system.CalcResiduals(solution)
                    .Print(15);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Read();
            }

            
            Console.Read();
        }

    }
}
