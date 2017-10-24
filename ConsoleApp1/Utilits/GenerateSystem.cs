using System;

namespace NumericMethods.Utilits
{
    class SystemGenerator
    {
        private int N;
        private int k;
        private int MATRIX_SIZE;

        public SquareMatrix Matrix { private set; get; }
        public Vector FreeElems { private set; get; }

        public SystemGenerator(int student_number, int group_number, int matrix_size)
        {
            N = student_number;
            k = group_number;
            MATRIX_SIZE = matrix_size;
            Matrix = new SquareMatrix(matrix_size);
            FreeElems = new Vector(matrix_size);
        }

        public void GenerateRegularSystem()
        {
            var matrix = new SquareMatrix(MATRIX_SIZE);
            var freeElems = new Vector(MATRIX_SIZE);

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
        }

        public void GenerateTridiagonalSystem()
        {
            if (MATRIX_SIZE != 4) throw new Exception("Only for 4-dimensions system");
            var matrix = new double[4, 4] { { (N + k),    -k,         0,          0          },
                                            { -(N / 2.0), 2 * N,      k,          0          },
                                            { 0,          -(N / 5.0), N,          -(k / 2.0) },
                                            { 0,          0,          -(N / 3.0), N          } };
                                                                                                                                       
            var vector = new double[4]      { N,          (N / 2.0),  N - k,      2 * N      };

            Matrix = new SquareMatrix(matrix);
            FreeElems = new Vector(vector);
        }
    }
}