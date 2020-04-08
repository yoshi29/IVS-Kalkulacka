using System;
using System.Linq;
using System.Collections.Generic;
using MathLibrary;

namespace StandardDeviation
{
    class Program
    {
        public static void Main(string[] args)
        {
            List<double> numbers;
            
            // Generate random numbers if parameter '-r' is set
            if (args.Length > 0 && args[0] == "-r")
            {
                int number;
                if (args.Length > 1)
                {
                    if (!int.TryParse(args[1], out number))
                        number = 1000;
                }
                else
                    number = 1000;

                numbers = GenRndNumbers(number);
            }

            // Get numbers from stdin
            else
                numbers = GetNumbers();

            try
            {
                double result = CountStandardDev(numbers);
                Console.WriteLine(result.ToString());
            }
            catch (Exception exc)
            {
                Console.Error.WriteLine(exc.ToString());
            }
        }

        /// <summary>
        /// Calculates the standard deviation from a given number list.
        /// </summary>
        /// <param name="values">List of numbers</param>
        /// <returns>Calculated standard deviation</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="values"/> contains 1 or less numbers.</exception>
        private static double CountStandardDev(List<double> values)
        {
            int n = values.Count();

            if (n <= 1)
                throw new ArgumentOutOfRangeException("Number of values must be greater than 1");

            double sum = 0;
            double arith_mean = 0;
            foreach (double x in values)
            {
                arith_mean = MathLib.Add(arith_mean, x);
                sum = MathLib.Add(sum, MathLib.Pow(x, 2));
            }

            arith_mean = MathLib.Div(arith_mean, n);

            double result = MathLib.Sub(sum, MathLib.Mul(n, MathLib.Pow(arith_mean, 2)));
            result = MathLib.Root(MathLib.Div(result, MathLib.Sub(n, 1)), 2);
            return result;
        }

        /// <summary>
        /// Loads all numbers from standard input
        /// </summary>
        /// <returns>List of numbers</returns>
        private static List<double> GetNumbers()
        {
            List<double> values = new List<double>();

            string temp;
            while ((temp = Console.ReadLine()) != null)
            {
                if (!double.TryParse(temp, out double number))
                    continue;

                if (double.IsInfinity(number) || double.IsNaN(number))
                    continue;

                values.Add(number);
            }

            return values;
        }

        /// <summary>
        /// Generates given amount of random numbers within the given range
        /// </summary>
        /// <param name="number_of_values">Number of random numbers to generate</param>
        /// <param name="min">Minimum value that can be generated</param>
        /// <param name="max">Maximum value that can be generated</param>
        /// <returns>List of randomly generated numbers</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="min"/> is greater than <paramref name="max"/></exception>
        private static List<double> GenRndNumbers(int number_of_values, double min = -1000, double max = 1000)
        {
            if (min > max)
                throw new ArgumentException("Minimum value must be lower than maximum value");

            List<double> values = new List<double>();

            double random;
            for (int i = 0; i < number_of_values; i++)
            {
                random = MathLib.Rnd();
                random = MathLib.Add(MathLib.Mul(random, MathLib.Sub(max, min)), min);
                values.Add(random);
            }

            return values;
        }
    }
}
