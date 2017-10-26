using System;
using NumericMethods.Methods;

namespace Lab7
{
    class Program
    {
        private const int GROUP_NUMBER = 1;
        private const double PRECISION = 1E-5;
        private static string FUNCTION = "(x^2)*atan(x)-" + GROUP_NUMBER.ToString();
        private static string SFUNCTION = "(" + GROUP_NUMBER.ToString() + "/atan(x))^(1/2)";

        static void Main()
        {
            Console.WriteLine("Lab7: Iteration method            \n-----------------------------------");
            try
            {
                var solution = IterationMethod.Calculate(SFUNCTION, PRECISION);
                Console.WriteLine("Solution of {0} via Iteration method is: {1}", FUNCTION, solution);
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