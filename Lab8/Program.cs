using System;
using NumericMethods.Methods;

namespace Lab8
{
    class Program
    {
        private const int GROUP_NUMBER = 1;
        private const double PRECISION = 1E-5;
        private static string FUNCTION = "(x^2)*atan(x)-" + GROUP_NUMBER.ToString();
        private static string DIFF_FUNCTION = "(x^2)/((x^2)+1)+2*x*atan(x)";
        private static string SFUNCTION = "x-(" + FUNCTION + ")/(" + DIFF_FUNCTION + ")";

        static void Main() {
            Console.WriteLine("Lab8: Newton method            \n-----------------------------------");
            try {
                var solution = IterationMethod.Calculate(SFUNCTION, PRECISION);
                Console.WriteLine("Solution of {0} via Newton method is: {1}", FUNCTION, solution);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                Console.Read();
            }

            Console.Read();
        }
    }
}