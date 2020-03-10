﻿using MathLibrary;
using NUnit.Framework;
using System;

namespace MathLibraryTests
{
    public class AdvancedFunctionsTests
    {

        /// <summary>
        /// Tests of factorial
        /// </summary>
        /// <param name="num">Number</param>
        /// <param name="expected">Expected result of test</param>
        [TestCase(0.0, 1.0)]
        [TestCase(10.0, 3628800.0)]
        public void Fact(double num, double expected)
        {
            Assert.That(MathLib.Fact(num), Is.EqualTo(expected));
        }

        /// <summary>
        /// Tests of factorial, that should throw ArgumentOutOfRangeException
        /// </summary>
        /// <param name="num">Number</param>
        [TestCase(0.2)]
        [TestCase(-1.0)]
        public void Fact_ShouldThrowArgumentOutOfRangeException(double num) 
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
               MathLib.Fact(num)
            );
        }

        /// <summary>
        /// Tests of exponentiation
        /// </summary>
        /// <param name="number">Base</param>
        /// <param name="exponent">Exponent</param>
        /// <param name="expected">Expected result of test</param>
        [TestCase(-1.0, 0.0, 1.0)]
        [TestCase(10.0, -10, 1E-10)]
        [TestCase(21.0, 1.5, 96.2340895940726)]
        [TestCase(10.0, 500.0, double.PositiveInfinity)]
        [TestCase(2.0, -5000.0, 0.0)]
        public void Pow(double number, double exponent, double expected)
        {
            Assert.That(MathLib.Pow(number, exponent), Is.EqualTo(expected).Within(1E-13));
        }

        /// <summary>
        /// Tests of nth-Root
        /// </summary>
        /// <param name="number">Radicant</param>
        /// <param name="degree">Degree</param>
        /// <param name="expected">Expected result of test</param>
        [TestCase(0.0, 2.0, 0.0)]
        [TestCase(100.0, 5.0, 2.5118864315)]
        [TestCase(2.0, 1E+30, 1.0)]
        public void Root(double number, double degree, double expected)
        {
            Assert.That(MathLib.Root(number, degree), Is.EqualTo(expected).Within(1E-10));
        }

        /// <summary>
        /// Tests of nth-Root, that should throw ArgumentOutOfRangeException
        /// </summary>E
        /// <param name="number">Radicant</param>
        /// <param name="degree">Degree</param>
        [TestCase(10, 0)]
        [TestCase(-10, 2)]
        public void Root_ShouldThrowArgumentOutOfRangeException(double number, double degree)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                   MathLib.Root(number, degree)
               );
        }
    }
}