using NumericMethods.Methods;
using NumericMethods.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    class Program
    {
        private const int STUDENT_NUMBER = 8;
        private const int GROUP_NUMBER = 1;

        static void Main(string[] args) {
            var data = GenerateArray(STUDENT_NUMBER, GROUP_NUMBER);

            Console.WriteLine("Points (x/y): ");
            var points_x = new VectorRow(data[0]);
            var points_y = new VectorRow(data[1]);

            Console.Write("X: ");
            points_x.Print();
            Console.Write("Y: ");
            points_y.Print();

            Spline spline_m_i = CubicSpline.Calculate_m_i(data[0], data[1]);
            spline_m_i.Print();
            Console.ReadLine();

            Spline spline_M_i = CubicSpline.Calculate_M_i(data[0], data[1]);
            spline_M_i.Print();
            Console.ReadLine();

        }

        private static double[][] GenerateArray(int N, int k) {
            return new double[2][] {
                new double[] {3.5,   4.1,     4.3,   5},
                new double[] {N + k, N + 2*k, N - k, N}
            };
        }
    }
}
