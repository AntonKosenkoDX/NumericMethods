using System;

namespace NumericMethods.Methods
{
    static class Tridiagonal
    {
        public static Vector Calculate(SquareMatrix matrix, Vector freeElems)
        {
            if (matrix.Size != freeElems.Size)
                throw new Exception(
                    "In Tridiagonal.Calculate: " +
                    "Size of matrix isn't equal size of free element's vector.");

            var n = freeElems.Size;

            var a = new Vector(n);
            var b = new Vector(n);
            var c = new Vector(n);
            var d = new Vector(n);

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

            var xi = new Vector(n + 1);
            var eta = new Vector(n + 1);

            for (int i = 0; i < n; i++)
            {
                xi[i + 1] = c[i] / (b[i] - a[i] * xi[i]);
                eta[i + 1] = (a[i] * eta[i] - d[i]) / (b[i] - a[i] * xi[i]);
            }

            var solutions = new Vector(n + 1);

            for (int i = n - 1; i >= 0; i--)
                solutions[i] = xi[i + 1] * solutions[i + 1] + eta[i + 1];

            var _solutions = new Vector(n);
            for (int i = 0; i < n; i++) _solutions[i] = solutions[i];

            return _solutions;
        }

        private static double Abs(double x) => x > 0 ? x : -x;

    }
}