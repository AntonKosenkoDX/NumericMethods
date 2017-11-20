using System;

namespace NumericMethods.Objects
{
    public class Vector : Matrix
    {
        public int Size { get; }

        public Vector(int size)
            : base(1, size)
        {
            Size = size;
            base.Columns = Size;
            base.Rows = 1;
        }

        public Vector(double[] vector)
            : base(new double[1, vector.Length])
        {
            Size = Columns;
            base.Columns = Size;
            base.Rows = 1;

            for (int i = 0; i < vector.Length; i++)
                matrix[0, i] = vector[i];
        }

        public Vector(double[,] vector)
            : base(vector)
        {
            if (vector.GetUpperBound(0) == 0)
            {
                Size = vector.GetUpperBound(1) + 1;
                base.Columns = Size;
                base.Rows = 1;
            }
            else if (vector.GetUpperBound(1) == 0)
            {
                var temp = new Matrix(vector);
                matrix = temp.GetTransposeMatrix().ToDoubleArray();
                Size = temp.Rows;
                base.Columns = Size;
                base.Rows = 1;

            }
            else
                throw new FormatException("This isn't vector!");
        }

        new public double[] ToDoubleArray()
        {
            var vector = new double[Columns];

            for (int i = 0; i < Columns; i++)
                vector[i] = matrix[0, i];

            return vector;
        }

        public double this[int index]
        {
            set
            {
                matrix[0, index] = value;
            }

            get
            {
                return matrix[0, index];
            }
        } 

        public static Vector operator +(Vector left, Vector right)
        {
            if (left.Size != right.Size)
                throw new FormatException("Vectors must have the same dimensions.");

            return new Vector(((Matrix)left + (Matrix)right).ToDoubleArray());
        }

        public static double operator *(Vector left, Vector right)
        {
            if (left.Size != right.Size)
                throw new FormatException("Vectors must have the same dimensions.");
            var r = right.GetTransposeMatrix();
            return (left * right.GetTransposeMatrix())[0];
        }

        public static Vector operator *(Matrix left, Vector right) =>
            new Vector(
                (left * ((Matrix)right).GetTransposeMatrix())
                .ToDoubleArray());

        public static Vector operator *(Vector left, Matrix right) =>

            new Vector(
                ((Matrix)left * right)
                .ToDoubleArray());

        new private Matrix GetTransposeMatrix()
        {
            var newRows = Columns;
            var newColumns = Rows;

            var transposeMatrix = new double[newRows, newColumns];
            for (int i = 0; i < newRows; i++)
                for (int j = 0; j < newColumns; j++)
                    transposeMatrix[i, j] = matrix[j, i];

            return new Matrix(transposeMatrix);
        }
    }
}