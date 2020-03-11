
using System;

namespace MathLibrary
{
    public static class MathLib
    {
        /// <summary>
        /// Adds two numbers
        /// </summary>
        /// <param name="v1">Addend</param>
        /// <param name="v2">Addend</param>
        /// <returns>Addition result</returns>
        public static double Add(double v1, double v2)
        {
            return v1 + v2;
        }

        /// <summary>
        /// Subtracts two numbers
        /// </summary>
        /// <param name="v1">Minuend</param>
        /// <param name="v2">Subtrahend</param>
        /// <returns>Subtraction result</returns>
        public static double Sub(double v1, double v2)
        {
            return v1 - v2;
        }

        /// <summary>
        /// Multiplies two numbers
        /// </summary>
        /// <param name="v1">Factor</param>
        /// <param name="v2">Factor</param>
        /// <returns>Multiplication result</returns>
        public static double Mul(double v1, double v2)
        {
            return v1 * v2;
        }

        /// <summary>
        /// Divides two numbers
        /// </summary>
        /// <param name="v1">Dividend</param>
        /// <param name="v2">Divisor</param>
        /// <returns>Division result</returns>
        public static double Div(double v1, double v2)
        {
            if (v2 == 0)
                throw new DivideByZeroException();
            return v1 / v2;
        }

        /// <summary>
        /// Calculates factorial of a given number
        /// </summary>
        /// <param name="num">Number</param>
        /// <returns>Factorial result</returns>
        public static double Fact(double num)
        {
            if (num > 170)
                return double.PositiveInfinity;

            if (num < 0 || num % 1 != 0)
                throw new ArgumentOutOfRangeException();

            double result = 1;

            for (int i = 1; i <= num; i++)
                result *= i;

            return result;
        }

        /// <summary>
        /// Calculates the power of a given number
        /// </summary>
        /// <param name="number">Base</param>
        /// <param name="exponent">Exponent</param>
        /// <returns>Result after exponentiation</returns>
        public static double Pow(double number, double exponent)
        { 
            if (exponent < 0 || (number == 0 && exponent == 0) || exponent % 1 != 0)
                throw new ArgumentOutOfRangeException();

            if (number == 0)
                return 0;

            return CountPow(number, (int)exponent);
        }

        // Auxiliary function that recursively counts the power of given number
        private static double CountPow(double number, int exponent)
        {
            if (exponent == 0)
                return 1;

            double half = CountPow(number, exponent / 2);

            if (exponent % 2 == 0)
                return half * half;

            else
                return number * half * half;
        }

        /// <summary>
        /// Calculates n-th root of given number using bisection method
        /// </summary>
        /// <param name="number">Radicand (The number for which we want to calculate the n-th root)</param>
        /// <param name="degree">Degree of the root</param>
        /// <returns>N-th root of given number</returns>
        public static double Root(double number, double degree)
        {
            if (degree <= 0 || degree % 1 != 0 || (number < 0 && degree % 2 == 0))
                throw new ArgumentOutOfRangeException();

            double max = number;
            double min = 0;
            double mid = 0;

            bool stop = false;
            double prev_mid = 0;

            while (!stop)
            {
                mid = (max + min) / 2;

                if (mid == prev_mid)    // We have reached the maximum precision
                    stop = true;

                double guess = Pow(mid, degree);

                if (guess == number)
                    return mid;

                if (number > 0)
                {
                    if (guess > number)
                        max = mid;
                    else
                        min = mid;
                }

                else
                {
                    if (guess > number)
                        min = mid;
                    else
                        max = mid;
                }

                prev_mid = mid;
            }
            return mid;
        }

        /// <summary>
        /// Calculates the absolute value
        /// </summary>
        /// <param name="num">Number</param>
        /// <returns>Absolute value of a given number</returns>
        public static double Abs(double num)
        {
            if (num < 0)
                return -num;
            return num;
        }
    }
}
