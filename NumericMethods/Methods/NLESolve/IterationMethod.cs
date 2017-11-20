using NumericMethods.Objects;
using System;

namespace NumericMethods.Methods
{
    public static class IterationMethod
    {
        public static double Calculate(string expression, double allowResidual)
        {
            Func f = new Function(expression).Calculate;

            var solutions = new double[3] { 1, 0, 0}; // x_n+1, x_n, x_n-1

            while(Residual(solutions) > allowResidual)
            {
                solutions[2] = solutions[1];
                solutions[1] = solutions[0];
                solutions[0] = f(solutions[0]);
            }

            return solutions[0];
        }

        private static double Residual(double[] solutions) =>
            Math.Abs(
                Math.Pow(solutions[0] - solutions[1], 2) /
            (2 * solutions[1] - solutions[0] - solutions[2])
                );

        private delegate double Func(double x);

    }
}
