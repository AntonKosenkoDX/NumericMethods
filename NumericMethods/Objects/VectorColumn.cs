using System;

namespace NumericMethods.Objects
{
    public class VectorColumn : AbstractVector {
        public VectorColumn(double[] vector) : base(vector) { } 
        public VectorColumn(int length) : base(length) { }

        public Matrix ToMatrix() {
            double[,] matrix = new double[this.Length, 1];
            for (int i = 0; i < Length; i++)
                matrix[i, 0] = this.data[i];

            return new Matrix(matrix);
        }

        public VectorRow ToRow() =>
            new VectorRow(data);

        public static VectorColumn operator +(VectorColumn left, VectorColumn right) {
            if(left.Length != right.Length)
                throw new FormatException("Vectors must have the same dimensions.");

            double[] sumVector = new double[left.Length];
            for (int i = 0; i < left.Length; i++)
                sumVector[i] = left[i] + right[i];

            return new VectorColumn(sumVector);
        }

        public static VectorColumn operator -(VectorColumn left, VectorColumn right) {
            if (left.Length != right.Length)
                throw new FormatException("Vectors must have the same dimensions.");

            double[] sumVector = new double[left.Length];
            for (int i = 0; i < left.Length; i++)
                sumVector[i] = left[i] - right[i];

            return new VectorColumn(sumVector);
        }

        public static VectorColumn operator *(double left, VectorColumn right) {
            double[] mulVector = new double[right.Length];
            for (int i = 0; i < right.Length; i++)
                mulVector[i] = left * right[i];

            return new VectorColumn(mulVector);
        }

        public static VectorColumn operator *(VectorColumn left, double right) =>
            right * left;

        public static Matrix operator *(VectorColumn left, VectorRow right) =>
            left.ToMatrix() * right.ToMatrix();
    }
}