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

            double[] checkArray = new double[] { 3.5, 3.6, 3.9, 4.1, 4.2, 4.3, 5 };


            Spline spline_m_i = CubicSpline.Calculate_m_i(data[0], data[1]);
            Console.WriteLine("S_mi(x): ");
            spline_m_i.Print();
            Console.ReadLine();

            Console.WriteLine("\nCheck: ");
            for (int i = 0; i < checkArray.Length; i++)
                Console.WriteLine("S_mi({0}) = {1}", checkArray[i], spline_m_i.Calculate(checkArray[i]));
            Console.Read();

            Spline spline_M_i = CubicSpline.Calculate_M_i(data[0], data[1]);
            Console.WriteLine("S_Mi(x): ");
            spline_M_i.Print();
            Console.ReadLine();

            Console.WriteLine("\nCheck: ");
            for (int i = 0; i < checkArray.Length; i++)
                Console.WriteLine("S_Mi({0}) = {1}", checkArray[i], spline_M_i.Calculate(checkArray[i]));
            Console.Read();
        }

        private static double[][] GenerateArray(int N, int k) { 
            return new double[2][] {
                new double[] {3.5,   4.1,     4.3,   5},
                new double[] {N + k, N + 2*k, N - k, N}
            };
        }
    }
}
