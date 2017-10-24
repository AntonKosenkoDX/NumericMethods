using System;
using NumericMethods.Utilits;

namespace NumericMethods.Methods
{
    static class DichotomyMethod
    {
        public static double Calculate(string expression, double allowResidual)
        {
            Func f = new Function(expression).Calculate;

            double x_0 = 0;
            double x_1 = 0;

            while(f(x_0) * f(x_1) >= 0)
            {
                x_0 -= 0.1;
                x_1 += 0.1;
            }

            while (Math.Abs(x_1 - x_0) > allowResidual)
            {
                var x_2 = (x_0 + x_1) / 2.0;

                if (f(x_2) == 0)
                    return x_2;

                if (f(x_2) * f(x_0) > 0)
                    x_0 = x_2;
                else
                    x_1 = x_2;
            }

            return (x_0 + x_1) / 2.0;
        }

        private delegate double Func(double x);
    }
}
