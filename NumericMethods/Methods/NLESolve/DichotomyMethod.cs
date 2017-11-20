using NumericMethods.Objects;
using System;

namespace NumericMethods.Methods
{
    public static class DichotomyMethod
    {
        public static double Calculate(string expression, double allowResidual)
        {
            Func f = new Function(expression).Calculate;

            double leftPoint = 0;
            double rightPoint = 0;

            while(f(leftPoint) * f(rightPoint) >= 0)
            {
                leftPoint -= 0.1;
                rightPoint += 0.1;
            }

            while (Math.Abs(rightPoint - leftPoint) > allowResidual)
            {
                var center = (leftPoint + rightPoint) / 2.0;

                if (f(center) == 0)
                    return center;

                if (f(center) * f(leftPoint) > 0)
                    leftPoint = center;
                else
                    rightPoint = center;
            }

            return (leftPoint + rightPoint) / 2.0;
        }

        private delegate double Func(double x);
    }
}
