using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericMethods.Objects
{
    public struct SplinePart{
        public PowerSeries polynom;
        public double UpperBound;
        public double LowerBound;
    }
    public class Spline
    {
        public readonly SplinePart[] Data;

        public Spline(PowerSeries[] parts, double[] x) {
            if (parts.Length + 1 != x.Length)
                throw new ArgumentException();

            SplinePart[] data = new SplinePart[parts.Length];

            for(int i = 0; i < parts.Length; i++) {
                data[i].polynom = parts[i];
                data[i].LowerBound = x[i];
                data[i].UpperBound = x[i + 1];
            }

            this.Data = data;
        }

        public double Calculate(double x) {
            for (int i = 0; i < Data.Length; i++)
                if (x >= Data[i].LowerBound && x <= Data[i].UpperBound)
                    return Data[i].polynom.Calculate(x);
            throw new ArgumentOutOfRangeException();
        }

        public void Print() {
            for (int i = 0; i < this.Data.Length; i++)
                Console.WriteLine(
                    "{0} where {1} < x < {2}",
                    Data[i].polynom.ToString(), Data[i].LowerBound, Data[i].UpperBound
                    );
        }
    }
}
