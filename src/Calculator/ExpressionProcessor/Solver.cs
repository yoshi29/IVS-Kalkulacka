using System;
using System.Collections.Generic;
using System.Linq;
using MathLibrary;

namespace ExpressionProcessor
{
    public class Solver
    {
        /// <summary>
        /// String of unary operators
        /// </summary>
        private static readonly string unary_op = "+-";

        /// <summary>
        /// String of all operators
        /// </summary>
        private static readonly string operators = unary_op + "*/^";

        /// <summary>
        /// List of functions and number of their parameters
        /// </summary>
        private static readonly Dictionary<string, int> functions = new Dictionary<string, int>()
        {
		    // name of the function, number of parameters
		    {"abs", 1},
            {"root", 2}
        };

        /// <summary>
        /// Checks if a given string is an operator.
        /// </summary>
        /// <param name="str">String we want to check</param>
        /// <returns><c>True</c> if given string is an operator</returns>
        private static bool IsOp(string str)
        {
            if (operators.Contains(str))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Checks if a given string is a function.
        /// </summary>
        /// <param name="str">String we want to check</param>
        /// <returns><c>True</c> if given string is a function</returns>
        private static bool IsFunc(string str)
        {
            if (functions.ContainsKey(str))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Calculates the given expression which is splitted into tokens.
        /// </summary>
        /// <remarks>
        /// Expression must be in postfix notation
        /// </remarks>
        /// <param name="tokens">List of tokens</param>
        /// <returns>Calculation result</returns>
        public static string Solve(List<string> tokens)
        {
            List<string> temp = tokens;
            int i = 0;
            while (temp.Count() > 1)
            {
                if (i > temp.Count() - 1)
                    throw new IndexOutOfRangeException();

                if (IsOp(temp.ElementAt(i)))
                {
                    string oper = temp.ElementAt(i);

                    double arg1 = Convert.ToDouble(temp.ElementAt(i - 2));
                    double arg2 = Convert.ToDouble(temp.ElementAt(i - 1));
                    double result = oper switch
                    {
                        "+" => MathLib.Add(arg1, arg2),
                        "-" => MathLib.Sub(arg1, arg2),
                        "*" => MathLib.Mul(arg1, arg2),
                        "/" => MathLib.Div(arg1, arg2),
                        "^" => MathLib.Pow(arg1, arg2),
                        _ => throw new System.ComponentModel.InvalidEnumArgumentException(),
                    };
                    temp.RemoveAt(i);
                    temp.RemoveAt(i - 1);
                    temp[i - 2] = result.ToString();
                    i = 0;
                }
                else if (IsFunc(temp.ElementAt(i)))
                {
                    string func = temp.ElementAt(i);
                    double result, arg1, arg2;

                    switch (func)
                    {
                        case "root":
                            arg2 = Convert.ToDouble(temp.ElementAt(i - 1));
                            arg1 = Convert.ToDouble(temp.ElementAt(i - 2));
                            result = MathLib.Root(arg1, arg2);
                            temp.RemoveAt(i);
                            temp.RemoveAt(i - 1);
                            temp[i - 2] = result.ToString();
                            break;

                        case "abs":
                            arg1 = Convert.ToDouble(temp.ElementAt(i - 1));
                            result = MathLib.Abs(arg1);
                            temp.RemoveAt(i);
                            temp[i - 1] = result.ToString();
                            break;

                        default:
                            throw new System.ComponentModel.InvalidEnumArgumentException();
                    }
                    i = 0;
                }
                else if (temp.ElementAt(i) == "!")
                {
                    double arg = Convert.ToDouble(temp.ElementAt(i - 1));
                    double result = MathLib.Fact(arg);
                    temp.RemoveAt(i);
                    temp[i - 1] = result.ToString();
                }

                else
                    i++;
            }

            return temp.ElementAt(0);
        }
    }
}
