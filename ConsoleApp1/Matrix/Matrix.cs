using System;

namespace NumericMethods
{
    class Matrix
    {
        public virtual int Rows { protected set; get; }
        public virtual int Columns { protected set; get; }

        protected double[,] matrix;
        private int PRECISION = 5;

        virtual public double[,] ToDoubleArray() => matrix;

        protected Matrix() { }

        public Matrix(double[,] matrix)
        {
            Rows = matrix.GetUpperBound(0) + 1;
            Columns = matrix.GetUpperBound(1) + 1;

            this.matrix = new double[Rows, Columns];

            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    this.matrix[i, j] = matrix[i, j];
        }

        public Matrix(int rows, int columns)
        {
            if (columns <= 0 && rows <= 0)
                throw new FormatException("Dimensions must be positive integer.");

            Rows = rows;
            Columns = columns;
            matrix = new double[rows, columns];
        }

        virtual public Matrix Exclude(int row, int column)
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

            return new Matrix(tmp);
        }

        virtual public Matrix GetTransposeMatrix()
        {
            var newRows = Columns;
            var newColumns = Rows;

            var transposeMatrix = new double[newRows, newColumns];
            for (int i = 0; i < newRows; i++)
                for (int j = 0; j < newColumns; j++)
                    transposeMatrix[i, j] = matrix[j, i];

            return new Matrix(transposeMatrix);
        }

        public Vector GetRow(int index)
        {
            if (index >= Rows || index < 0)
                throw new Exception("Can't get row.");

            var row = new Vector(Columns);

            for (int i = 0; i < row.Size; i++)
                row[i] = matrix[index, i];

            return row;
        }

        public void SetRow(Vector vector, int index)
        {
            if (index >= Rows || index < 0 || vector.Size != Columns)
                throw new Exception("Can't set row.");

            for (int i = 0; i < vector.Size; i++)
                matrix[index, i] = vector[i];
        }

        public Vector GetColumn(int index)
        {
            if (index >= Columns || index < 0)
                throw new Exception("Can't get column.");

            var row = new Vector(Rows);

            for (int i = 0; i < row.Size; i++)
                row[i] = matrix[i, index];

            return row;
        }

        public void SetColumn(Vector vector, int index)
        {
            if (index >= Columns || index < 0 || vector.Size != Rows)
                throw new Exception("Can't set column.");

            for (int i = 0; i < vector.Size; i++)
                matrix[i, index] = vector[i];
        }

        public double this[int row, int column]
        {
            set
            {
                matrix[row, column] = value;
            }

            get
            {
                return matrix[row, column];
            }
        }

        public static Matrix operator +(Matrix left, Matrix right)
        {
            if (left.Columns != right.Columns && left.Rows != right.Rows)
                throw new FormatException("Matrices must have the same dimensions.");

            var sum = new Matrix(left.Rows, left.Columns);
            for (int i = 0; i < left.Rows; i++)
                for (int j = 0; j < left.Columns; j++)
                    sum.matrix[i, j] = left.matrix[i, j] + right.matrix[i, j];

            return sum;
        }

        public static Matrix operator -(Matrix left, Matrix right)
        {
            if (left.Columns != right.Columns && left.Rows != right.Rows)
                throw new FormatException("Matrices must have the same dimensions.");

            var sub = new Matrix(left.Rows, left.Columns);
            for (int i = 0; i < left.Rows; i++)
                for (int j = 0; j < left.Columns; j++)
                    sub.matrix[i, j] = left.matrix[i, j] - right.matrix[i, j];

            return sub;
        }

        public static Matrix operator *(int left, Matrix right)
        {
            var matrix = new Matrix(right.ToDoubleArray());
            for (int i = 0; i < matrix.Rows; i++)
                for (int j = 0; j < matrix.Columns; j++)
                    matrix.matrix[i, j] *= left;

            return matrix;
        }

        public static Matrix operator *(double left, Matrix right)
        {
            var matrix = new Matrix(right.ToDoubleArray());
            for (int i = 0; i < matrix.Rows; i++)
                for (int j = 0; j < matrix.Columns; j++)
                    matrix.matrix[i, j] *= left;

            return matrix;
        }

        public static Matrix operator *(Matrix left, Matrix right)
        {
            if (left.Columns != right.Rows)
                throw new NotSupportedException("Left's width must be equal right's height.");

            var mul = new Matrix(left.Rows, right.Columns);
            for (int i = 0; i < left.Rows; i++)
                for (int j = 0; j < right.Columns; j++)
                {
                    double temp = 0;
                    for (int k = 0; k < left.Columns; k++)
                        temp += left[i, k] * right[k, j];
                    mul.matrix[i, j] = temp;
                }

            return mul;
        }

        public void SetPrintPrecision(int numOfSymbolsAfterPoint)
        {
            PRECISION = numOfSymbolsAfterPoint;
        }

        public void Print()
        {
            PrintMatrix(matrix, PRECISION);
        }

        private void PrintMatrix(double[,] matrix, int precision)
        {
            var rows = matrix.GetUpperBound(0) + 1;
            var columns = matrix.GetUpperBound(1) + 1;

            Console.WriteLine();

            var formatString = GetFormatString(precision);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(formatString, matrix[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }

        private String GetFormatString(int precision)
        {
            String formatString = "{0,-10 :0.";

            for (int i = 0; i < precision; i++)
                formatString += "#";

            return formatString += "}";
        }
    }
}