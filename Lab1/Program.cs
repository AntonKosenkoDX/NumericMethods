using System;
using NumericMethods.Methods;
using NumericMethods.Objects;

namespace Lab1
{
    class Program
    {
        private const int STUDENT_NUMBER = 9;
        private const int GROUP_NUMBER = 1;
        private const int MATRIX_SIZE = 5;

        static void Main()
        {
            Console.WriteLine("Lab1: Gauss method                \n-----------------------------------");
            try
            {
                var system = new LSystem(STUDENT_NUMBER, GROUP_NUMBER, MATRIX_SIZE);
                system.GenerateRegularSystem();

                Console.WriteLine("Our system:");
                system.Print();

                Console.WriteLine(system.Matrix.ToString());

                Matrix.SetNumberOfThreads(4);
                var solution = GaussSolve.Calculate(system);
                Console.WriteLine("Solutions via Gauss method:");
                solution.Print(-1);

                Console.WriteLine("Residuals: ");
                system.CalcResiduals(solution).
                    Print(-1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Read();
            }//GaussSolve                                    

            Console.Read();
        }

    }
}
