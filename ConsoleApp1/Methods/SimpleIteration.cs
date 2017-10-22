using System;

namespace NumericMethods
{
    static class SimpleIteration //http://orloff.am.tpu.ru/chisl_metod/Lab3/iter.htm
    {
        public static Vector CalculateClassic(SquareMatrix smatrix, Vector freeElems, double allowResidual)
        {
            if (smatrix.Size != freeElems.Size)
                throw new Exception(
                    "In SimpleIteration.CalculateClassic: " +
                    "Size of matrix isn't equal size of free element's vector.");

            var det = smatrix.GetDeterminant();
            if (det == 0)
                throw new Exception("Determinant of matrix is 0. The system can't be solved.");

            var invMatrix = smatrix.GetInvertibleMatrix();
            var deltaMatrix = GetDeltaMatrix(smatrix.Size);
            var D = invMatrix - deltaMatrix;
            var X = new Vector(freeElems.Size);

            var alpha = deltaMatrix * smatrix;

            var beta = D * freeElems;

            while (MaxResidual(smatrix, freeElems, X) > allowResidual)
            {
                X = alpha * X + beta;
            }

            return X;
        }

        public static Vector CalculateZeidel(SquareMatrix smatrix, Vector freeElems, double allowResidual)
        {
            if (smatrix.Size != freeElems.Size)
                throw new Exception(
                    "In SimpleIteration.CalculateZeidel: " +
                    "Size of matrix isn't equal size of free element's vector.");

            var det = smatrix.GetDeterminant();
            if (det == 0)
                throw new Exception("Determinant of matrix is 0. The system can't be solved.");

            var invMatrix = smatrix.GetInvertibleMatrix();
            var deltaMatrix = GetDeltaMatrix(smatrix.Size);
            var D = invMatrix - deltaMatrix;
            var X = new Vector(freeElems.Size);

            var alpha = deltaMatrix * smatrix;

            var beta = D * freeElems;

            while (MaxResidual(smatrix, freeElems, X) > allowResidual)
            {
                for (int i = 0; i < X.Size; i++)
                {
                    var buf = 0.0;
                    for (int j = 0; j < X.Size; j++) buf += alpha[i, j] * X[j];
                    X[i] = buf + beta[i];
                }
            }

            return X;
        }

        private static double MaxResidual(Matrix matrix, Vector freeElems, Vector solutions) => 
            Max(new Vector((matrix * solutions - freeElems).ToDoubleArray()));

        private static double Max(Vector vector)
        {
            var max = Math.Abs(vector[0]);
            for (int i = 0; i < vector.Size; i++)
                if (Math.Abs(vector[i]) > max)
                    max = Math.Abs(vector[i]);
            return max;
        }

        private static SquareMatrix GetDeltaMatrix(int size)
        {
            var m = new SquareMatrix(size);

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    m[i, j] = 0.0000001;
            return m;
        }
    }
}