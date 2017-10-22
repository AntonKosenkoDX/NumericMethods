using System;

namespace NumericMethods
{
    static class CharacterPolynom
    {
        public static Vector Calculate(SquareMatrix matrix)
        {
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

        public static void Print(Vector polyCoeffs)
        {
            Console.Write("lambda^{0} - ", polyCoeffs.Size);
            for (int i = polyCoeffs.Size; i > 1; i--)
                Console.Write("{0} * lambda^{1} - ", polyCoeffs[polyCoeffs.Size - i], i - 1);
            Console.Write(polyCoeffs[polyCoeffs.Size - 1]);
            Console.WriteLine();
        }
    }
}
