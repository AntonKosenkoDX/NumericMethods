﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumericMethods.Objects;

namespace NumericMethods.Methods
{
    public class LagrangePolynomial
    {
        public PowerSeries Calculate(double[,] xy) {
            if (xy.GetUpperBound(0) != 1)
                throw new FormatException("Array of values must contain values only for two variables.");

            int maxPower = xy.GetUpperBound(1);
            var lagrangePolynomial = new PowerSeries(maxPower);

            for (int i = 0; i <= xy.GetUpperBound(1); i++) 
                lagrangePolynomial += GetPart(xy, i);

            return lagrangePolynomial;
        }

        private PowerSeries GetPart(double[,] xy, int index) {
            var poly = new PowerSeries(xy.GetUpperBound(1));
            poly.SetCoeff(1, 0);

            for(int i = 0; i < index; i++)
                poly *= new PowerSeries(new double[] { -xy[0, i], 1 });
            for(int i = index + 1; i <= xy.GetUpperBound(1); i++ )
                poly *= new PowerSeries(new double[] { -xy[0, i], 1 });

            return xy[1, index] * poly * (1 / poly.Calculate(xy[0, index]));
        }

        private int KronekerDelta(double i, double j) => i == j ? 1 : 0;
    }
}
