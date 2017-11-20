using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericMethods.Objects
{
    public class PowerSeries
    {
        private double[] coeffs { get; set; }

        public PowerSeries(double[] coeffs) => this.coeffs = coeffs.ToArray();

        public PowerSeries(int maxPower) => this.coeffs = new double[maxPower + 1];

        public double Calculate(double argument) {
            int counter = 0;
            double sum = 0;
            foreach (double cf in coeffs) 
                sum += cf * Math.Pow(argument, counter++);

            return sum;
        }

        public static PowerSeries operator *(PowerSeries left, PowerSeries right) {
            double[] leftArray = left.GetCoeffs();
            double[] rigthArray = right.GetCoeffs();
            double[] mulArray = new double[rigthArray.Length + leftArray.Length - 1];

            Parallel.For (0, leftArray.Length, leftIndex => {
                for (int rightIndex = 0; rightIndex < rigthArray.Length; rightIndex++)
                    mulArray[leftIndex + rightIndex] += leftArray[leftIndex] * rigthArray[rightIndex];
            });

            return new PowerSeries(mulArray);
        } 

        public static PowerSeries operator *(double left, PowerSeries right) {
            double[] rightArray = right.GetCoeffs();
            double[] mulArray = new double[rightArray.Length];

            for (int i = 0; i < rightArray.Length; i++)
                mulArray[i] = left * rightArray[i];

            return new PowerSeries(mulArray);
        }

        public static PowerSeries operator *(PowerSeries left, double right) =>
            right * left;

        public static PowerSeries operator +(PowerSeries left, PowerSeries right) {
            double[] leftArray = left.GetCoeffs();
            double[] rigthArray = right.GetCoeffs();
            double[] sumArray = new double[Math.Max(leftArray.Length, rigthArray.Length)];

            for (int leftIndex = 0; leftIndex < leftArray.Length; leftIndex++)
                sumArray[leftIndex] += leftArray[leftIndex];

            for (int rightIndex = 0; rightIndex < rigthArray.Length; rightIndex++)
                sumArray[rightIndex] += rigthArray[rightIndex];

            return new PowerSeries(sumArray);
        }

        public static PowerSeries operator -(PowerSeries left, PowerSeries right) {
            double[] leftArray = left.GetCoeffs();
            double[] rigthArray = right.GetCoeffs();
            double[] subArray = new double[Math.Max(leftArray.Length, rigthArray.Length)];

            for (int leftIndex = 0; leftIndex < leftArray.Length; leftIndex++)
                subArray[leftIndex] += leftArray[leftIndex];

            for (int rightIndex = 0; rightIndex < rigthArray.Length; rightIndex++)
                subArray[rightIndex] -= rigthArray[rightIndex];

            return new PowerSeries(subArray);
        }

        public void Print() {
            bool isWriteBefore = false;
            for(int index = coeffs.Length - 1; index >= 0; index--) {
                if (coeffs[index] == 0)
                    continue;
                else if (coeffs[index] < 0)
                    Console.Write("- {0}", Math.Abs(coeffs[index]));
                else if (isWriteBefore)
                    Console.Write("+ {0}", coeffs[index]);
                else
                    Console.Write("{0}", coeffs[index]);

                if (index > 0)
                    Console.Write("x^{0} ", index);

                isWriteBefore = true;
            }
            Console.ReadLine();
        }

        public double[] GetCoeffs() => coeffs;

        public void SetCoeff(double value, int index) => coeffs[index] = value;
    }
}
