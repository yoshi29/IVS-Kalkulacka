using MathLibrary;
using NUnit.Framework;
using System;

namespace MathLibraryTests
{
    /// <summary>
    /// Tests of basic functions of mathematical library
    /// </summary>
    public class BasicFunctionsTests
    {
        /// <summary>
        /// Tests of addition
        /// </summary>
        /// <param name="num1">Addend</param>
        /// <param name="num2">Addend</param>
        /// <param name="expected">Expected result of test</param>
        [TestCase(0.0, 0.0, 0.0)]
        [TestCase(-12.75, 4.247, -8.503)]
        [TestCase(double.MaxValue, double.MinValue, 0.0)]
        [TestCase(double.MaxValue, double.MaxValue, double.PositiveInfinity)]
        public void Add(double num1, double num2, double expected)
        {
            Assert.That(MathLib.Add(num1, num2), Is.EqualTo(expected));
        }

        /// <summary>
        /// Tests of subtraction
        /// </summary>
        /// <param name="num1">Minuend</param>
        /// <param name="num2">Subtrahend</param>
        /// <param name="expected">Expected result of test</param>
        [TestCase(0.0, 0.0, 0.0)]
        [TestCase(15.786, 5.992, 9.794)]
        [TestCase(142.117, 581.02, -438.903)]
        [TestCase(0.04586, -12.0, 12.04586)]
        [TestCase(double.MaxValue, double.MaxValue, 0.0)]
        [TestCase(double.MaxValue, double.MinValue, double.PositiveInfinity)]
        public void Sub(double num1, double num2, double expected)
        {
            Assert.That(MathLib.Sub(num1, num2), Is.EqualTo(expected));
        }

        /// <summary>
        /// Tests of multiplication
        /// </summary>
        /// <param name="num1">Factor</param>
        /// <param name="num2">Factor</param>
        /// <param name="expected">Expected result of test</param>
        [TestCase(0.0, 0.0, 0.0)]
        [TestCase(9.1290, -42.12005, -384.51393645)]
        [TestCase(double.MaxValue, -1, double.MinValue)]
        [TestCase(double.MaxValue, double.MaxValue, double.PositiveInfinity)]
        public void Mul(double num1, double num2, double expected)
        {
            Assert.That(MathLib.Mul(num1, num2), Is.EqualTo(expected).Within(1E-8));
        }

        /// <summary>
        /// Tests of division
        /// </summary>
        /// <param name="num1">Dividend</param>
        /// <param name="num2">Divisor</param>
        /// <param name="expected">Expected result of test</param>
        [TestCase(0.0, 3.0, 0.0)]
        [TestCase(12.4, -2.0, -6.2)]
        [TestCase(double.MaxValue, double.MinValue, -1.0)]
        public void Div(double num1, double num2, double expected) 
        {
            Assert.That(MathLib.Div(num1, num2), Is.EqualTo(expected).Within(1E-8));
        }

        /// <summary>
        /// Test of division, that should throw DivideByZeroException
        /// </summary>
        /// <param name="num1">Dividend</param>
        /// <param name="num2">Divisor</param>
        [TestCase(12.5, 0)]
        public void Div_ShouldThrowDivideByZeroException(double num1, double num2)
        {
            Assert.Throws<DivideByZeroException>(() =>
                   MathLib.Div(num1, num2)
               );
        }
    }
}