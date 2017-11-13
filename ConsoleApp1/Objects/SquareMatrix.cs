using System;
using System.Threading.Tasks;

namespace NumericMethods.Objects
{
    public class SquareMatrix : Matrix
    {
        public int Size { get; }

        public SquareMatrix(double[,] matrix)
            : base(matrix)
        {
            if (matrix.GetUpperBound(0) != matrix.GetUpperBound(1))
                throw new FormatException("Input matrix isn't square!");
            Size = Columns;
        }

        public SquareMatrix(int size)
            : base(size, size) { Size = size; }

        public double GetDeterminant()
        {
            var temp = new SquareMatrix(matrix);
            double det = ToTriangleAndReturnSign(temp);
            for (int i = 0; i < Size; i++)
                det *= temp[i, i];
            return det;
        }

        new public SquareMatrix GetTransposeMatrix()
        {
            var newRows = Columns;
            var newColumns = Rows;

            var transposeMatrix = new double[newRows, newColumns];
            for (int i = 0; i < newRows; i++)
                for (int j = 0; j < newColumns; j++)
                    transposeMatrix[i, j] = matrix[j, i];

            return new SquareMatrix(transposeMatrix);
        }

        new public SquareMatrix Exclude(int row, int column)
        {
            var tmp = new double[Rows - 1, Columns - 1];

            var _i = 0;
            var _j = 0;
            for (int i = 0; i < Rows; i++)
            {
                if (i == row) continue;
                for (int j = 0; j < Columns; j++)
                {
                    if (j == column) continue;
                    tmp[_i, _j] = matrix[i, j];
                    _j++;
                }
                _i++;
                _j = 0;
            }

            return new SquareMatrix(tmp);
        }

        public SquareMatrix GetAdjectiveMatrix()
        {
            var adj = new SquareMatrix(Size);

            Parallel.For(0, Size, Matrix.options, i => {
                for (int j = 0; j < Size; j++)
                    adj[i, j] = Math.Pow(-1, i + j) * Exclude(i, j).GetDeterminant();
            });

            return adj.GetTransposeMatrix();
        }

        public SquareMatrix GetInvertibleMatrix() =>
            new SquareMatrix(
                ((1.0 / GetDeterminant()) * GetAdjectiveMatrix())
                .ToDoubleArray());

        public bool isDiagonalDomination()
        {
            for (int i = 0; i < Size; i++)
            {
                var Sum = 0.0;
                for (int j = 0; j < Size; j++)
                    Sum += Abs(matrix[i, j]);
                Sum -= matrix[i, i];
                if (Abs(matrix[i, i]) < Sum) return false;
            }
            return true;
        }

        private int ToTriangleAndReturnSign(SquareMatrix tmatrix)
        {
            var size = tmatrix.Size;
            var sign = 1;

            for (int i = 0; i < size - 1; i++)
            {
                sign *= SelectMainElement(tmatrix, i);

                Parallel.For(i + 1, size, Matrix.options, j => {
                    if (tmatrix[j, i] == 0) return;
                    var cf = tmatrix[j, i] / tmatrix[i, i];

                    for (int k = 0; k < size; k++)
                        tmatrix[j, k] = tmatrix[j, k] - tmatrix[i, k] * cf;
                });
            }
            return sign;
        }

        private int SelectMainElement(SquareMatrix matrix, int pos)
        {
            var maxrow = pos;
            var size = matrix.Size;

            for (int i = pos; i < size; i++)
                if (Math.Abs(matrix[i, pos]) > Math.Abs(matrix[maxrow, pos])) maxrow = i;


            if (maxrow == pos) return 1;

            var temprow = new double[size];
            for (int i = 0; i < size; i++)
            {
                temprow[i] = matrix[pos, i];
                matrix[pos, i] = matrix[maxrow, i];
                matrix[maxrow, i] = temprow[i];
            }

            return -1;
        }

        private double Abs(double x) => x > 0 ? x : -x;

    }
}