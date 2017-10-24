using System;
using NumericMethods.Methods;
using NumericMethods.Utilits;

namespace NumericMethods
{
    class Program
    {
        private const int STUDENT_NUMBER = 9;
        private const int GROUP_NUMBER = 1;
        private const int MATRIX_SIZE = 5;
        private const double PRECISION = 1E-5;
        private static string FUNCTION = "(x^2)*atan(x)-" + GROUP_NUMBER.ToString();
        private static string SFUNCTION = "(" + GROUP_NUMBER.ToString() + "/atan(x))^(1/2)";

        static void Main(string[] args)
        {
            Console.WriteLine("Lab1: Gauss method                \n-----------------------------------");
            try
            {
                var system = new SystemGenerator(STUDENT_NUMBER, GROUP_NUMBER, MATRIX_SIZE);
                system.GenerateRegularSystem();

                Console.WriteLine("Our system: \nMatrix: ");
                system.Matrix.Print();
                Console.WriteLine("Free elements: ");
                system.FreeElems.Print();

                var solution = GaussSolve.Calculate(system.Matrix, system.FreeElems);
                Console.WriteLine("Solutions via Gauss method:");
                solution.Print();

                var residuals = CalcResiduals(system.Matrix, system.FreeElems, solution);
                residuals.SetPrintPrecision(15);
                Console.WriteLine("Residuals: ");
                residuals.Print();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Read();
            }//GaussSolve                                    

            Console.WriteLine("Lab2: Simple iteration method     \n-----------------------------------");
            try
            {
                var system = new SystemGenerator(STUDENT_NUMBER, GROUP_NUMBER, MATRIX_SIZE);
                system.GenerateRegularSystem();

                var solution = SimpleIteration.CalculateClassic(system.Matrix, system.FreeElems, 0.00000001);
                Console.WriteLine("Solutions via Simple Iteration method:");
                solution.Print();

                var residuals = CalcResiduals(system.Matrix, system.FreeElems, solution);
                residuals.SetPrintPrecision(15);
                Console.WriteLine("Residuals: ");
                residuals.Print();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Read();
            }//Simple Iteration                              

            Console.WriteLine("Lab3: Zeidel method               \n-----------------------------------");
            try
            {
                var system = new SystemGenerator(STUDENT_NUMBER, GROUP_NUMBER, MATRIX_SIZE);
                system.GenerateRegularSystem();

                var solution = SimpleIteration.CalculateZeidel(system.Matrix, system.FreeElems, 0.00000001);
                Console.WriteLine("Solutions via Zeidel method:");
                solution.Print();

                var residuals = CalcResiduals(system.Matrix, system.FreeElems, solution);
                residuals.SetPrintPrecision(15);
                Console.WriteLine("Residuals: ");
                residuals.Print();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Read();
            }//Zeidel Simple Iteration

            Console.WriteLine("Lab4: Tridiagonal algorithm       \n-----------------------------------");
            try
            {
                var system = new SystemGenerator(STUDENT_NUMBER, GROUP_NUMBER, 4);
                system.GenerateTridiagonalSystem();

                Console.WriteLine("Our system: \nMatrix: ");
                system.Matrix.Print();
                Console.WriteLine("Free elements: ");
                system.FreeElems.Print();

                var solution = Tridiagonal.Calculate(system.Matrix, system.FreeElems);
                Console.WriteLine("Solutions via Tridiagonal algorithm:");
                solution.Print();

                var residuals = CalcResiduals(system.Matrix, system.FreeElems, solution);
                residuals.SetPrintPrecision(15);
                Console.WriteLine("Residuals: ");
                residuals.Print();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Read();
            }//Tridiagonal algorithm

            Console.WriteLine("Lab5: Characteristic polynomial   \n-----------------------------------");
            try
            {
                var system = new SystemGenerator(STUDENT_NUMBER, GROUP_NUMBER, MATRIX_SIZE);
                system.GenerateRegularSystem();

                Console.WriteLine("Our matrix: ");
                system.Matrix.Print();

                var polyCoeffs = CharacterPolynom.Calculate(system.Matrix);
                Console.WriteLine("Characteristic polynomial:");
                CharacterPolynom.Print(polyCoeffs);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Read();
            }//Characteristic polynomial

            Console.WriteLine("Lab6: Dichotomy method            \n-----------------------------------");
            try
            {
                var solution = DichotomyMethod.Calculate(FUNCTION, PRECISION);
                Console.WriteLine("Solution of {0} via Dichotmy method is: {1}", FUNCTION, solution); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Read();
            }//Dichotomy method 

            Console.WriteLine("Lab7: Iteration method            \n-----------------------------------");
            try
            {
                var solution = IterationMethod.Calculate(SFUNCTION, PRECISION);
                Console.WriteLine("Solution of {0} via Iteration method is: {1}", FUNCTION, solution);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Read();
            }//Dichotomy method 

            Console.Read();
        }

        private static Vector CalcResiduals(Matrix matrix, Vector freeElems, Vector solutions) =>
           new Vector((matrix * solutions - freeElems).ToDoubleArray());
    }
}