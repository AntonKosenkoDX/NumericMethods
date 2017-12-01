using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumericMethods.Objects;

namespace NumericMethods.Methods
{
    struct BoundaryConditions{
        public VectorRow first;
        public VectorRow second;
    }

    public class CubicSpline
    {
        private static double[] X;
        private static double[] Y;

        public static Spline Calculate_m_i(double[] x, double[] y) {
            if (x.Length != y.Length)
                throw new ArgumentException("y", "Array of function's values must has same length as array of arguments.");

            X = x;
            Y = y;

            Matrix mainEquations = PrepareEquations_m_i();
            BoundaryConditions boundaryConditions = GetBoundaryConditions_m_i();

            LSystem system = BuildSystem(mainEquations, boundaryConditions);
            VectorColumn solutions = Tridiagonal.Calculate(system);

            return new Spline(BuildSpline_m_i(solutions), X);
        }

        public static Spline Calculate_M_i(double[] x, double[] y) {
            if (x.Length != y.Length)
                throw new ArgumentException("y", "Array of function's values must has same length as array of arguments.");

            X = x;
            Y = y;

            Matrix mainEquations = PrepareEquations_M_i();
            BoundaryConditions boundaryConditions = GetBoundaryConditions_M_i();

            LSystem system = BuildSystem(mainEquations, boundaryConditions);
            VectorColumn solutions = Tridiagonal.Calculate(system);

            return new Spline(BuildSpline_M_i(solutions), X);
        }

        private static double H(int index) => X[index + 1] - X[index];
        
        private static PowerSeries T(int index) {
            PowerSeries series = new PowerSeries(new double[] { -X[index], 1 });
            return series * (1.0 / H(index));
        }

        private static PowerSeries[] BuildSpline_m_i(VectorColumn m) {
            PowerSeries[] spline = new PowerSeries[X.Length - 1];
            
            for(int i = 0; i < X.Length - 1; i++) {
                spline[i] = 
                    Y[i] * (1 - T(i)) * (1 - T(i)) * (1 + 2 * T(i)) 
                    + Y[i + 1] * T(i) * T(i) * (3 - 2 * T(i)) 
                    + m[i] * H(i) * T(i) * (1 - T(i)) * (1 - T(i)) 
                    - m[i + 1] * H(i) * T(i) * T(i) * (1 - T(i));
            }

            return spline;
        }

        private static PowerSeries[] BuildSpline_M_i(VectorColumn M) {
            PowerSeries[] spline = new PowerSeries[X.Length - 1];

            for (int i = 0; i < X.Length - 1; i++) {
                spline[i] =
                    Y[i] * (1 - T(i))
                    + Y[i + 1] * T(i)
                    - H(i) * H(i) * T(i) / 6 * (1 - T(i))
                    * ((2 - T(i)) * M[i] + (1 + T(i)) * M[i + 1]);
            }

            return spline;
        }

        private static Matrix PrepareEquations_m_i() {
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

        private static Matrix PrepareEquations_M_i() {
            Matrix mainEquations = new Matrix(X.Length - 2, X.Length + 1);

            double lambda, mu;
            for (int i = 1; i < X.Length - 1; i++) {
                lambda = H(i) / (H(i - 1) + H(i));
                mu = H(i - 1) / (H(i - 1) + H(i));
                mainEquations[i - 1, i - 1] = mu;
                mainEquations[i - 1, i] = 2;
                mainEquations[i - 1, i + 1] = lambda;
                mainEquations[i - 1, X.Length] =
                    6.0 / (H(i - 1) + H(i))
                    * (
                    (Y[i + 1] - Y[i]) / H(i) -
                    (Y[i] - Y[i - 1]) / H(i - 1)
                    );
            }

            return mainEquations;
        }

        private static LSystem BuildSystem(Matrix mainEquations, BoundaryConditions boundaryConditions) {
            if (boundaryConditions.first.Length != X.Length + 1
                || boundaryConditions.second.Length != X.Length + 1
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
                matrix[0, column] = boundaryConditions.first[column];
                matrix[X.Length - 1, column] = boundaryConditions.second[column];
            }

            freeElems[0] = boundaryConditions.first[X.Length];
            freeElems[X.Length - 1] = boundaryConditions.second[X.Length];

            return new LSystem(matrix, freeElems);
        }

        private delegate double Func(double x);

        private static BoundaryConditions GetBoundaryConditions_m_i() {
            Func fSecondDerivative =
                LagrangePolynomial.Calculate(X, Y)
                .FindDerivative()
                .FindDerivative()
                .Calculate;

            double freeElem_1 = 3.0 * (Y[1] - Y[0]) / H(0)
                - H(0) * fSecondDerivative(X[0]) / 2.0;

            double freeElem_2 = 3.0 * (Y[X.Length - 1] - Y[X.Length - 2]) / H(X.Length - 2)
                - H(X.Length - 2) * fSecondDerivative(X[X.Length - 2]) / 2.0;
            
            BoundaryConditions boundaryConditions = new BoundaryConditions() {
                first = new VectorRow(X.Length + 1),
                second = new VectorRow(X.Length + 1)
            };
            boundaryConditions.first[0] = 2.0;
            boundaryConditions.first[1] = 1.0;
            boundaryConditions.first[X.Length] = freeElem_1;

            boundaryConditions.second[X.Length - 2] = 1.0;
            boundaryConditions.second[X.Length - 1] = 2.0;
            boundaryConditions.second[X.Length] = freeElem_2;

            return boundaryConditions;
        }

        private static BoundaryConditions GetBoundaryConditions_M_i() {
            BoundaryConditions boundaryConditions = new BoundaryConditions() {
                first = new VectorRow(X.Length + 1),
                second = new VectorRow(X.Length + 1)
            };
            boundaryConditions.first[0] = 1.0;
            boundaryConditions.second[X.Length - 1] = 1.0;

            return boundaryConditions;
        }
    }
}
