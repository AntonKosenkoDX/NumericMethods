using NumericMethods.Objects;
using System;
using System.Threading.Tasks;

namespace NumericMethods.Methods
{
    public static class GaussSolve
    {
        public static Vector Calculate(LSystem system) =>
            Calculate(system.Matrix, system.FreeElems);

        public static Vector Calculate(SquareMatrix matrix, Vector freeElems)
        {
            if (matrix.Size != freeElems.Size)
                throw new Exception(
                    "In GaussSolve.Calculate: " +
                    "Size of matrix isn't equal size of free element's vector.");

            var det = matrix.GetDeterminant();
            if (det == 0)
                throw new Exception("Determinant of matrix is 0. The system can't be solved.");
            if (Abs(det) < 1)
                Console.WriteLine("The system is poorly conditioned, solutions can not be calculated accurately.");

            var _matrix = new SquareMatrix(matrix.ToDoubleArray());
            var _freeElems = new Vector(freeElems.ToDoubleArray());

            ToTriangle(_matrix, _freeElems);
            return Reverce(_matrix, _freeElems);
        }

        private static void ToTriangle(SquareMatrix matrix, Vector freeElems)
        {
            var size = matrix.Size; 

            for (int i = 0; i < size - 1; i++)
            {
                SelectMainElement(matrix, freeElems, i);

                Parallel.For(i + 1, size, Matrix.options, j => {

                    if (matrix[j, i] == 0) return;
                    var cf = matrix[j, i] / matrix[i, i];

                    for (int k = 0; k < size; k++)
                        matrix[j, k] -= matrix[i, k] * cf;

                    freeElems[j] -= freeElems[i] * cf;
                });
            }
        }

        private static void SelectMainElement(SquareMatrix matrix, Vector freeElems, int pos)
        {
            var maxrow = pos;
            var size = matrix.Size;

            for (int i = pos; i < size; i++)
                if (Abs(matrix[i, pos]) > Abs(matrix[maxrow, pos])) maxrow = i;

            if (maxrow == pos) return;

            var temprow = new double[size + 1];

            for (int i = 0; i < size; i++)
            {
                temprow[i] = matrix[pos, i];
                matrix[pos, i] = matrix[maxrow, i];
                matrix[maxrow, i] = temprow[i];
            }

            temprow[size] = freeElems[pos];
            freeElems[pos] = freeElems[maxrow];
            freeElems[maxrow] = temprow[size];

            return;
        }

        private static Vector Reverce(SquareMatrix triangleMatrix, Vector freeElems)
        {
            var size = triangleMatrix.Size;
            var solutions = new Vector(size);

            for (int i = size - 1; i >= 0; i--)
            {
                double accum = 0.0;
                for (int j = size - 1; j > i; j--)
                    accum += triangleMatrix[i, j] * solutions[j];
                solutions[i] = (freeElems[i] - accum) / triangleMatrix[i, i];
            }
            return solutions;
        }

        private static double Abs(double x) => x > 0 ? x : -x;
    }
}