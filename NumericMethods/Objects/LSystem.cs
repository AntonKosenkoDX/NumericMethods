using System;
using System.Globalization;
using System.Threading;

namespace NumericMethods.Objects
{
    public class LSystem
    {
        private int N;
        private int k;
        private int MATRIX_SIZE;

        public SquareMatrix Matrix { private set; get; }
        public VectorColumn FreeElems { private set; get; }

        public LSystem(int student_number, int group_number, int matrix_size)
        {
            N = student_number;
            k = group_number;
            MATRIX_SIZE = matrix_size;
            Matrix = new SquareMatrix(matrix_size);
            FreeElems = new VectorColumn(matrix_size);
        }

        public LSystem(SquareMatrix matrix, VectorColumn freeElems) {
            this.Matrix = matrix;
            this.FreeElems = freeElems;
            this.MATRIX_SIZE = -1;
        }

        public void GenerateRegularSystem()
        {
            if (MATRIX_SIZE == -1)
                throw new InvalidOperationException("System is yet implemented.");

            var matrix = new SquareMatrix(MATRIX_SIZE);
            var freeElems = new VectorColumn(MATRIX_SIZE);

            for (int i = 0; i < MATRIX_SIZE; i++)
                for (int j = 0; j < MATRIX_SIZE; j++)
                {
                    if (i < j) matrix[i, j] = (i + 1) + (j + 1) - N / 3.0 - k;
                    if (i == j) matrix[i, j] = (i + 1) + (j + 1) + N / 4.0 + k;
                    if (i > j) matrix[i, j] = (i + 1) + (j + 1) - N / 5.0 - k;
                }

            for (int i = 0; i < MATRIX_SIZE; i++)
                freeElems[i] = 3 * (i + 1) + N / 2.0 + k;

            Matrix = matrix;
            FreeElems = freeElems;
            MATRIX_SIZE = -1;
        }

        public void GenerateTridiagonalSystem()
        {
            if (MATRIX_SIZE == -1)
                throw new InvalidOperationException("System is yet implemented.");

            if (MATRIX_SIZE != 4) throw new Exception("Only for 4-dimensions system");
            var matrix = new double[4, 4] { { (N + k),    -k,         0,          0          },
                                            { -(N / 2.0), 2 * N,      k,          0          },
                                            { 0,          -(N / 5.0), N,          -(k / 2.0) },
                                            { 0,          0,          -(N / 3.0), N          } };
                                                                                                                                       
            var vector = new double[4]      { N,          (N / 2.0),  N - k,      2 * N      };

            Matrix = new SquareMatrix(matrix);
            FreeElems = new VectorColumn(vector);
            MATRIX_SIZE = -1;
        }

        public VectorColumn CalcResiduals(VectorColumn solutions) =>
            new VectorColumn((Matrix * solutions - FreeElems).ToDoubleArray());

        public void Print() => Print(5);

        public void Print(int precision)
        {
            Console.WriteLine();

            var formatString = GetFormatString(precision);
            for (int i = 0; i < Matrix.Rows; i++)
            {
                for (int j = 0; j < Matrix.Columns; j++) {
                    if (j == Matrix.Columns - 1) {
                        Console.Write(formatString + " * x" + j + " ", Matrix[i, j]);
                        continue;
                    }
                    Console.Write(formatString + " * x" + j + " + ", Matrix[i, j]);
                }
               Console.Write(" = " + formatString + "\n", FreeElems[i]);
            }
            Console.ReadLine();
        }

        private String GetFormatString(int precision)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            if (precision == -1)
                return "{0,10 :0.##E+00}";

            String formatString = "{0,-10 :0.";

            for (int i = 0; i < precision; i++)
                formatString += "#";

            return formatString += "}";
        }

    }
}