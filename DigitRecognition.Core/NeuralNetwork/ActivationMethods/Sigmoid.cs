using System;

namespace DigitRecognition.Core
{
    public class Sigmoid
    {

        public static double CalculateSigmoid(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-x));
        }

        public static double DerivativeSigmoid(double x)
        {
            return x * (1 - x);
        }
    }
}
