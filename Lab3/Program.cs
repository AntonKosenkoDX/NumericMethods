using NumericMethods.Methods;
using NumericMethods.Objects;
using System;

namespace Lab3
{
    class Program
    {
        private const int STUDENT_NUMBER = 9;
        private const int GROUP_NUMBER = 1;
        private const int MATRIX_SIZE = 5;
        private const double PRECISION = 1E-5;

        static void Main()
        {
            Console.WriteLine("Lab3: Zeidel method               \n-----------------------------------");
            try
            {
                var system = new LSystem(STUDENT_NUMBER, GROUP_NUMBER, MATRIX_SIZE);
                system.GenerateRegularSystem();

                Console.WriteLine("Our system:");
                system.Print();

                var solution = SimpleIteration.CalculateZeidel(system, PRECISION);
                Console.WriteLine("Solutions via Zeidel method:");
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
