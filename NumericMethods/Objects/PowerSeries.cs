using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericMethods.Objects
{
    public class PowerSeries
    {
        private VectorRow coeffs { get; set; }

        public int Length { get => coeffs.Length; } 

        public int MaxPower { get => Length - 1; }

        public PowerSeries(double[] coeffs) => this.coeffs = new VectorRow(coeffs);

        public PowerSeries(int maxPower) => this.coeffs = new VectorRow(maxPower + 1);

        public double Calculate(double argument) {
            double sum = 0;

            for (int i = 0; i < coeffs.Length; i++)
                sum += coeffs[i] * Math.Pow(argument, i);

            return sum;
        }

        public PowerSeries FindDerivative() {
            if (coeffs.Length == 1)
                return new PowerSeries(new double[1] { 0 });

            PowerSeries derivate = new PowerSeries(this.MaxPower - 1);
            for (int i = 0; i < derivate.Length; i++)
                derivate[i] = (i + 1) * coeffs[i + 1];

            return derivate;
        }

        public static PowerSeries operator *(PowerSeries left, PowerSeries right) {
            double[] leftArray = left.GetCoeffs().ToDoubleArray();
            double[] rigthArray = right.GetCoeffs().ToDoubleArray();
            double[] mulArray = new double[rigthArray.Length + leftArray.Length - 1];

            Parallel.For (0, leftArray.Length, leftIndex => {
                for (int rightIndex = 0; rightIndex < rigthArray.Length; rightIndex++)
                    mulArray[leftIndex + rightIndex] += leftArray[leftIndex] * rigthArray[rightIndex];
            });

            return new PowerSeries(mulArray);
        } 

        public static PowerSeries operator *(double left, PowerSeries right) {
            double[] rightArray = right.GetCoeffs().ToDoubleArray();
            double[] mulArray = new double[rightArray.Length];

            for (int i = 0; i < rightArray.Length; i++)
                mulArray[i] = left * rightArray[i];

            return new PowerSeries(mulArray);
        }

        public static PowerSeries operator *(PowerSeries left, double right) =>
            right * left;

        public static PowerSeries operator /(PowerSeries left, double right) =>
            left * (1.0 / right);

        public static PowerSeries operator +(PowerSeries left, PowerSeries right) {
            double[] leftArray = left.GetCoeffs().ToDoubleArray();
            double[] rigthArray = right.GetCoeffs().ToDoubleArray();
            double[] sumArray = new double[Math.Max(leftArray.Length, rigthArray.Length)];

            for (int leftIndex = 0; leftIndex < leftArray.Length; leftIndex++)
                sumArray[leftIndex] += leftArray[leftIndex];

            for (int rightIndex = 0; rightIndex < rigthArray.Length; rightIndex++)
                sumArray[rightIndex] += rigthArray[rightIndex];

            return new PowerSeries(sumArray);
        }

        public static PowerSeries operator -(PowerSeries left, PowerSeries right) {
            double[] leftArray = left.GetCoeffs().ToDoubleArray();
            double[] rigthArray = right.GetCoeffs().ToDoubleArray();
            double[] subArray = new double[Math.Max(leftArray.Length, rigthArray.Length)];

            for (int leftIndex = 0; leftIndex < leftArray.Length; leftIndex++)
                subArray[leftIndex] += leftArray[leftIndex];

            for (int rightIndex = 0; rightIndex < rigthArray.Length; rightIndex++)
                subArray[rightIndex] -= rigthArray[rightIndex];

            return new PowerSeries(subArray);
        }

        public static PowerSeries operator +(PowerSeries left, double right) {
            double[] sumArray = left.GetCoeffs().ToDoubleArray();
            sumArray[0] += right;
            return new PowerSeries(sumArray);
        }

        public static PowerSeries operator +(double left, PowerSeries right) =>
            right + left;

        public static PowerSeries operator -(PowerSeries left, double right) =>
            left + (-right);

        public static PowerSeries operator -(double left, PowerSeries right) =>
            (-1) * right + left;

        public void Print() => Print(5);

        public void Print(int precision) => 
            Console.WriteLine(this.ToString(precision));

        public override string ToString() => this.ToString(5);

        public string ToString(int precision) {
            bool isWriteBefore = false;
            string outstring = "";
            string formatString = GetFormatString(precision);
            for(int index = coeffs.Length - 1; index >= 0; index--) {
                if (coeffs[index] == 0)
                    continue;
                else if (coeffs[index] < 0)
                    outstring += String.Format("- " + formatString, Math.Abs(coeffs[index]));
                else if (isWriteBefore)
                    outstring += String.Format("+ " + formatString, coeffs[index]);
                else
                    outstring += String.Format(formatString, coeffs[index]);

                if (index > 0)
                    outstring += String.Format("x^{0} ", index);

                isWriteBefore = true;
            }
            return outstring;
        }

        public double this[int power]{
            set => SetCoeff(value, power);
            get => coeffs[power];
        }

        public VectorRow GetCoeffs() => coeffs;

        public void SetCoeff(double value, int index) => coeffs[index] = value;

        private String GetFormatString(int precision) {
            String formatString = "{0:0.";

            for (int i = 0; i < precision; i++)
                formatString += "#";

            return formatString += "}";
        }
    }
}
