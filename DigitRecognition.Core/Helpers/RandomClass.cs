using System;

namespace DigitRecognition.Core
{
    public static class RandomClass
    {
        private static Random random = new Random();
        public static double RandomWeight()
        {
            return random.NextDouble() - 0.5;
        }

        public static int RandomWeight(int x)
        {
            return random.Next(x);
        }
    }
}
