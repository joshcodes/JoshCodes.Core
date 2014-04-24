using System;

namespace JoshCodes.Core
{
    public static class RandomExtensions
    {
        public static int Between(this Random rand, int min, int max)
        {
            double delta = max - min;
            return min + (int)Math.Round(rand.NextDouble() * delta);
        }
    }
}
