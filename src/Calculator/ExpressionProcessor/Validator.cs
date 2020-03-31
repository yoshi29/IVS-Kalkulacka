using System;
using System.Collections.Generic;

namespace ExpressionProcessor
{
    public class Validator
    {
        /// <summary>
        /// String of unary operators
        /// </summary>
        private static readonly string unary_op = "+-";

        /// <summary>
        /// String of binary operators
        /// </summary>
        private static readonly string binary_op = "*/^";

        /// <summary>
        /// String of all operators
        /// </summary>
        private static readonly string operators = unary_op + binary_op;

        /// <summary>
        /// List of functions and number of their parameters
        /// </summary>
        private static readonly Dictionary<string, int> functions = new Dictionary<string, int>()
        {
            // name of the function, number of parameters
            {"abs", 1},
            {"root", 2},
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
        /// Checks if a given character is an operator.
        /// </summary>
        /// <param name="ch">Character we want to check</param>
        /// See <see cref="Splitter.IsOp(string)"/>.
        private static bool IsOp(char ch)
        {
            return IsOp(ch.ToString());
        }

        /// <summary>
        /// Checks if a given string is a valid expression.
        /// </summary>
        /// <param name="exp">The string to be checked</param>
        /// <param name="num_of_param">Number of parameters (Only if the expression is inside a function)</param>
        /// <remarks>
        /// Parameter <c>num_of_param</c> is optional
        /// </remarks>
        /// <returns><c>True</c> if a string is a valid expression</returns>
        public static bool IsValid(string exp, int num_of_param = 1)
        {
            int len = exp.Length;
            int i = 0;
            int commas = 0;
            int num_of_brackets = 0;

            while (i < len)
            {
                if (IsOp(exp[i]))
                {
                    if (unary_op.Contains(exp[i]))
                    {
                        if (i == len - 1) // If the operator is last in expression
                            return false;

                        if ((").,!" + operators).Contains(exp[i + 1])) // Check char after operator
                            return false;

                        if (i != 0)
                        {
                            if (("." + operators).Contains(exp[i - 1]) || char.IsLetter(exp[i - 1])) // Check char before operator
                                return false;
                        }
                    }

                    else
                    {
                        if (i == 0 || i == len - 1)
                            return false;

                        if (("),.!" + operators).Contains(exp[i + 1])) // Check char after operator
                            return false;

                        if (("(,." + operators).Contains(exp[i - 1]) || char.IsLetter(exp[i - 1])) // Check char before operator
                            return false;
                    }

                    i++;
                }

                else if (exp[i] == '!')
                {
                    if (i == 0)
                        return false;

                    if (("(,." + operators).Contains(exp[i - 1]) || char.IsLetter(exp[i - 1])) // Check char before factorial
                        return false;

                    if (i != len - 1)
                    {
                        if ("(.".Contains(exp[i + 1]) || char.IsLetterOrDigit(exp[i + 1])) // Check char after factorial
                            return false;
                    }

                    i++;
                }

                else if (exp[i] == '(' || exp[i] == ')')
                {
                    if (exp[i] == '(')
                    {
                        num_of_brackets++;

                        if (i == len - 1)   // If the bracket is last in expression
                            return false;

                        if (("),.!" + binary_op).Contains(exp[i + 1]))   // Check char after bracket
                            return false;

                        if (i != 0)
                        {
                            if (").!".Contains(exp[i - 1]) || char.IsDigit(exp[i - 1])) // Check char before bracket
                                return false;
                        }
                    }
                    else // exp[i] == ')'
                    {
                        num_of_brackets--;

                        if (i == 0) // If the bracket is first in expression
                            return false;

                        if (("(,." + operators).Contains(exp[i - 1]) || char.IsLetter(exp[i - 1]))  // Check char before bracket
                            return false;

                        if (i != len - 1)
                        {
                            if ("(.".Contains(exp[i + 1]) || char.IsLetterOrDigit(exp[i + 1]))  // Check char after bracket
                                return false;
                        }
                    }

                    if (num_of_brackets < 0)
                        return false;

                    i++;
                }

                else if (char.IsDigit(exp[i]))
                {
                    bool P_exists = false;
                    bool E_exists = false;
                    while (i < len && (char.IsDigit(exp[i]) || exp[i] == '.' || exp[i] == 'e'))  // Go through the whole number and check it
                    {
                        if (exp[i] == '.')
                        {
                            if (exp[i - 1] == 'e' || E_exists || P_exists)
                                return false;
                            P_exists = true;
                        }

                        else if (exp[i] == 'e')
                        {
                            if (exp[i - 1] == '.' || i == len - 1 || E_exists)
                                return false;
                            if (unary_op.Contains(exp[i + 1]))
                            {
                                i++;
                                if (i == len - 1)
                                    return false;
                                if (!char.IsDigit(exp[i + 1]))
                                    return false;
                            }
                            E_exists = true;
                        }
                        i++;
                    }
                }

                else if (exp[i] == '.') // Check for separated points.
                    return false;

                else if (exp[i] == ',')
                {
                    if (i == 0 || i == len - 1)
                        return false;

                    if (("(,." + operators).Contains(exp[i - 1]) || char.IsLetter(exp[i - 1])) // Check char before comma
                        return false;

                    if (("),.!" + binary_op).Contains(exp[i + 1])) // Check char after comma
                        return false;

                    commas++;
                    i++;
                }

                else if (char.IsLetter(exp[i]))
                {
                    string func = "";
                    while (i < len && (char.IsLetter(exp[i])))  // Get the whole word.
                    {
                        func += exp[i];
                        i++;
                    }

                    if (!functions.TryGetValue(func, out int parameters))   // Check if the word is valid function name
                        return false;

                    if (exp[i] != '(')
                        return false;

                    int brackets = 1;
                    string sub_exp = "(";
                    i++;
                    while (i < len && brackets != 0)    // Get expression from brackets
                    {
                        sub_exp += exp[i];

                        if (exp[i] == '(')
                            brackets++;

                        else if (exp[i] == ')')
                            brackets--;

                        i++;
                    }
                    if (sub_exp == "" || sub_exp[sub_exp.Length - 1] != ')')
                        return false;

                    if (parameters == 0)
                    {
                        if (sub_exp != "()")
                            return false;
                    }
                    else
                    {
                        if (!IsValid(sub_exp, parameters)) // Recursively check if function parameters are valid
                            return false;
                    } 
                }
                else
                    return false;
            }

            if (num_of_brackets != 0)
                return false;
            
            if (commas != num_of_param - 1)
            {
                if (num_of_param != 0 || commas != 0)
                    return false;
            }

            return true;
        }
    }
}
