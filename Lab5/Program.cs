using NumericMethods.Methods;
using NumericMethods.Objects;
using System;

namespace Lab5
{
    class Program
    {
        private const int STUDENT_NUMBER = 9;
        private const int GROUP_NUMBER = 1;
        private const int MATRIX_SIZE = 5;

        static void Main()
        {

            Console.WriteLine("Lab5: Characteristic polynomial   \n-----------------------------------");
            try
            {
                var system = new LSystem(STUDENT_NUMBER, GROUP_NUMBER, MATRIX_SIZE);
                system.GenerateRegularSystem();

                Console.WriteLine("Our matrix: ");
                system.Matrix.Print();

                var polyCoeffs = CharacterPolynomial.Calculate(system.Matrix);
                Console.WriteLine("Characteristic polynomial:");
                CharacterPolynomial.Print(polyCoeffs);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Read();
            }

            Console.Read();
        }

    }

}
