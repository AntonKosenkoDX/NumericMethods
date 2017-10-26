using NumericMethods.Methods;
using System;

namespace Lab6
{
    class Program
    {
        private const int GROUP_NUMBER = 1;
        private const double PRECISION = 1E-5;
        private static string FUNCTION = "(x^2)*atan(x)-" + GROUP_NUMBER.ToString();

        static void Main()
        {
            Console.WriteLine("Lab6: Dichotomy method            \n-----------------------------------");
            try
            {
                var solution = DichotomyMethod.Calculate(FUNCTION, PRECISION);
                Console.WriteLine("Solution of {0} via Dichotmy method is: {1}", FUNCTION, solution);
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
