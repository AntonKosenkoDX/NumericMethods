using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericMethods.Objects
{
    public class VectorRow : AbstractVector {
        public VectorRow(double[] vector) : base(vector) { }
        public VectorRow(int length) : base(length) { }

        public Matrix ToMatrix() {
            double[,] matrix = new double[1, this.Length];
            for (int i = 0; i < Length; i++)
                matrix[0, i] = this.data[i];

            return new Matrix(matrix);
        }

        public VectorColumn ToColumn() =>
            new VectorColumn(data);

        public static VectorRow operator +(VectorRow left, VectorRow right) {
            if (left.Length != right.Length)
                throw new FormatException("Vectors must have the same dimensions.");

            double[] sumVector = new double[left.Length];
            for (int i = 0; i < left.Length; i++)
                sumVector[i] = left[i] + right[i];

            return new VectorRow(sumVector);
        }

        public static VectorRow operator -(VectorRow left, VectorRow right) {
            if (left.Length != right.Length)
                throw new FormatException("Vectors must have the same dimensions.");

            double[] subVector = new double[left.Length];
            for (int i = 0; i < left.Length; i++)
                subVector[i] = left[i] - right[i];

            return new VectorRow(subVector);
        }

        public static VectorRow operator *(double left, VectorRow right) {
            double[] mulVector = new double[right.Length];
            for (int i = 0; i < right.Length; i++)
                mulVector[i] = left * right[i];

            return new VectorRow(mulVector);
        }

        public static VectorRow operator *(VectorRow left, double right) =>
            right * left;
    }
}