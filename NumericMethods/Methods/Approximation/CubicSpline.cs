using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumericMethods.Objects;

namespace NumericMethods.Methods
{
    public class CubicSpline
    {
        private readonly double[] X;
        private readonly double[] Y;

        public CubicSpline(double[] x, double[] y) {
            if (x.Length != y.Length)
                throw new ArgumentException("y", "Array of function's values must has same length as array of arguments.");

            this.X = x;
            this.Y = y;
        }

        public PowerSeries[] Calculate_m_i() {
            Matrix mainEquations = PrepareEquations_m_i();
            VectorRow[] boundaryConditions = GetBoundaryConditions();

            LSystem system = BuildSystem(boundaryConditions[0], mainEquations, boundaryConditions[1]);
            VectorColumn solutions = Tridiagonal.Calculate(system);

            return BuildSpline_m_i(solutions);
        }

        private double H(int index) => X[index + 1] - X[index];
        
        private PowerSeries T(int index) {
            PowerSeries series = new PowerSeries(new double[] { -X[index], 1 });
            return series * (1.0 / H(index));
        }

        private PowerSeries[] BuildSpline_m_i(VectorColumn m) {
            PowerSeries[] spline = new PowerSeries[X.Length - 1];
            double A, B;
            for(int i = 0; i < X.Length - 1; i++) {
                A = -2.0 / H(i) * (Y[i + 1] - Y[i]) + m[i] + m[i + 1];
                B = -A + (Y[i + 1] - Y[i]) / H(i) - m[i];
                spline[i] = Y[i] + T(i) / H(i) * (m[i] + T(i) * (B + A * T(i)));
            }

            return spline;
        }

        private Matrix PrepareEquations_m_i() {
            Matrix mainEquations = new Matrix(X.Length - 2, X.Length + 1);

            double lambda, mu;
            for (int i = 1; i < X.Length - 1; i++) {
                lambda = H(i) / (H(i - 1) + H(i));
                mu = H(i - 1) / (H(i - 1) + H(i));
                mainEquations[i - 1, i - 1] = lambda;
                mainEquations[i - 1, i] = 2;
                mainEquations[i - 1, i + 1] = mu;
                mainEquations[i - 1, X.Length] =
                    3.0 * (
                    mu * (Y[i + 1] - Y[i]) / H(i) + 
                    lambda * (Y[i] - Y[i - 1]) / H(i - 1)
                    );
            }

            return mainEquations;
        }

        private LSystem BuildSystem(VectorRow firstBoundaryCondition, Matrix mainEquations, VectorRow lastBoundaryCondition) {
            if (firstBoundaryCondition.Length != X.Length + 1
                || lastBoundaryCondition.Length != X.Length + 1
                || mainEquations.Rows != X.Length - 2
                || mainEquations.Columns != X.Length + 1)
                throw new ArgumentException();

            SquareMatrix matrix = new SquareMatrix(X.Length);
            VectorColumn freeElems = new VectorColumn(X.Length);

            for (int row = 1; row < X.Length - 1; row++) {
                for (int column = 0; column < X.Length; column++)
                    matrix[row, column] = mainEquations[row - 1, column];
                freeElems[row] = mainEquations[row - 1, X.Length];
            }

            for (int column = 0; column < X.Length; column++) {
                matrix[0, column] = firstBoundaryCondition[column];
                matrix[X.Length - 1, column] = lastBoundaryCondition[column];
            }

            freeElems[0] = firstBoundaryCondition[X.Length];
            freeElems[X.Length - 1] = lastBoundaryCondition[X.Length];

            return new LSystem(matrix, freeElems);
        }

        private delegate double Func(double x);

        private VectorRow[] GetBoundaryConditions() {
            Func fSecondDerivative = 
                LagrangePolynomial.Calculate(X, Y)
                .FindDerivative()
                .FindDerivative()
                .Calculate;

            double freeElem_1 = 3.0 * (Y[1] - Y[0]) / H(0)
                - H(0) * fSecondDerivative(X[0]) / 2.0;

            double freeElem_2 = 3.0 * (Y[X.Length - 1] - Y[X.Length - 2]) / H(X.Length - 2)
                - H(X.Length - 2) * fSecondDerivative(X[X.Length - 2]) / 2.0;

            VectorRow[] boundaryConditions = new VectorRow[2] {
                new VectorRow(X.Length + 1),
                new VectorRow(X.Length + 1)
            };

            boundaryConditions[0][0] = 2.0;
            boundaryConditions[0][1] = 1.0;
            boundaryConditions[0][X.Length] = freeElem_1;

            boundaryConditions[1][X.Length - 2] = 1.0;
            boundaryConditions[1][X.Length - 1] = 2.0;
            boundaryConditions[1][X.Length] = freeElem_2;

            return boundaryConditions;
        }
    }
}
