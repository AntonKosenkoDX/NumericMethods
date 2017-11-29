using NumericMethods.Objects;
using System;

namespace NumericMethods.Methods
{
    public static class CharacterPolynomial
    {
        public static VectorColumn Calculate(SquareMatrix matrix){
            if (matrix == null)
                throw new ArgumentNullException("matrix", "Matrix can not be null.");

            var Y = new SquareMatrix(matrix.Size);
            Y[0, Y.Size - 1] = 1;

            for (int i = Y.Size - 1; i > 0; i--)
            {
                var newcolumn = matrix * Y.GetColumn(i);
                Y.SetColumn(newcolumn, i - 1);
            }
                
            var y = matrix * Y.GetColumn(0);

            return GaussSolve.Calculate(Y, y);
        }

        public static void Print(AbstractVector polyCoeffs){
            if (polyCoeffs == null)
                throw new ArgumentNullException("polyCoeffs", "Vector can not be null.");

            Console.Write("lambda^{0} - ", polyCoeffs.Length);
            for (int i = polyCoeffs.Length; i > 1; i--)
                Console.Write("{0} * lambda^{1} - ", polyCoeffs[polyCoeffs.Length - i], i - 1);
            Console.Write(polyCoeffs[polyCoeffs.Length - 1]);
            Console.WriteLine();
        }
    }
}
