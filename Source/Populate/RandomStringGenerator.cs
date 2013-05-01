using System;
using System.Text;

namespace OptimizationTest
{
    public static class RandomStringGenerator
    {
        private static readonly Random Generator = new Random((int)DateTime.Now.Ticks);

        public static string RandomString(int size, string prefix = null)
        {
            var builder = new StringBuilder(prefix);
            for (var i = 0; i < size; i++)
            {
                builder.Append(Convert.ToChar(Convert.ToInt32(Math.Floor((26 * Generator.NextDouble()) + 65))));
            }

            return builder.ToString();
        }
    }
}
