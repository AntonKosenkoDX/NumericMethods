using NumericMethods.Objects;
using System;

namespace NumericMethods.Methods
{
    public static class Tridiagonal
    {
        public static VectorColumn Calculate(LSystem system) =>
            Calculate(system.Matrix, system.FreeElems);

        public static VectorColumn Calculate(SquareMatrix matrix, VectorColumn freeElems)
        {
            if (matrix.Size != freeElems.Length)
                throw new Exception(
                    "In Tridiagonal.Calculate: " +
                    "Size of matrix isn't equal size of free element's vector.");

            var n = freeElems.Length;

            var a = new VectorColumn(n);
            var b = new VectorColumn(n);
            var c = new VectorColumn(n);
            var d = new VectorColumn(n);

            for (int i = 0; i < n; i++)
            {
                if (i > 0) a[i] = matrix[i, i - 1];
                b[i] = -matrix[i, i];
                if (i < n - 1) c[i] = matrix[i, i + 1];
                d[i] = freeElems[i];
            }

            for (int i = 0; i < n; i++)
                if (Abs(b[i]) < Abs(a[i]) + Abs(c[i]))
                    throw new Exception(
                        "In Tridiagonal.Calculate: " +
                        "There is no diagonal domination, the system can't be solved.");

            var xi = new VectorColumn(n + 1);
            var eta = new VectorColumn(n + 1);

            for (int i = 0; i < n; i++)
            {
                xi[i + 1] = c[i] / (b[i] - a[i] * xi[i]);
                eta[i + 1] = (a[i] * eta[i] - d[i]) / (b[i] - a[i] * xi[i]);
            }

            var solutions = new VectorColumn(n + 1);

            for (int i = n - 1; i >= 0; i--)
                solutions[i] = xi[i + 1] * solutions[i + 1] + eta[i + 1];

            var _solutions = new VectorColumn(n);
            for (int i = 0; i < n; i++) _solutions[i] = solutions[i];

            return _solutions;
        }

        private static double Abs(double x) => x > 0 ? x : -x;

    }
}