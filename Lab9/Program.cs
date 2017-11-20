using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumericMethods.Methods;
using NumericMethods.Objects;

namespace Lab9
{
    class Program
    {
        private const int STUDENT_NUMBER = 9;
        private const int GROUP_NUMBER = 1;

        static void Main(string[] args) {
            var data = GenerateArray(STUDENT_NUMBER, GROUP_NUMBER);
            var lagrange = new LagrangePolynomial().Calculate(data);

            Console.WriteLine("Points (x/y): ");
            var points = new Matrix(data);

            points.Print();
            Console.Write("L(x) = ");
            lagrange.Print();

            Console.WriteLine("\nCheck: ");
            for (int i = 0; i <= data.GetUpperBound(1); i++)
                Console.WriteLine("L({0}) = {1}", data[0, i], lagrange.Calculate(data[0, i]));
            Console.Read();
        }

        private static double[,] GenerateArray(int N, int k) {
            return new double[2, 4] {
                {3.5,   4.1,     4.3,   5},
                {N + k, N + 2*k, N - k, N}
            };
        }
    }
}
