using System;
using System.Globalization;
using System.Threading;

namespace NumericMethods.Objects
{
    public abstract class AbstractVector
    {
        protected double[] data;

        public int Length
        {
            get {
                return data.Length;
            }
        }

        protected AbstractVector(double[] vector) {
            data = (double[])vector.Clone();
        }

        protected AbstractVector(int length) {
            data = new double[length];
        }

        public double[] ToDoubleArray() => data;

        public double this[int index]
        {
            set {
                data[index] = value;
            }

            get {
                return data[index];
            }
        }

        public void Print() => Print(5);

        public void Print(int precision) {
            Console.WriteLine();

            var formatString = GetFormatString(precision);
            for (int i = 0; i < this.Length; i++) {
                Console.Write(formatString, this[i]);
                Console.Write(" ");
            }
            Console.WriteLine();

            Console.ReadLine();
        }

        private string GetFormatString(int precision) {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            if (precision == -1)
                return "{0:0.##E+00}";

            String formatString = "{0,-10 :0.";

            for (int i = 0; i < precision; i++)
                formatString += "#";

            return formatString += "}";
        }

    }
}
