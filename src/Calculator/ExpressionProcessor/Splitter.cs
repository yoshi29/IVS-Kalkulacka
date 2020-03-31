using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionProcessor
{
    public class Splitter
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
        /// Converts unary plus and minus to binary in a given string.
        /// </summary>
        /// <param name="exp">The string to be converted</param>
        /// <returns>Converted string without unary signs</returns>
        private static string UnaryToBin(string exp)
        {
            int len = exp.Length;
            string new_exp = "";

            for (int i = 0; i < len; i++)
            {
                if (unary_op.Contains(exp[i]))
                {
                    if (i == 0 || "(,".Contains(exp[i - 1]))
                        new_exp += "0";
                }
                new_exp += exp[i];
            }
            return new_exp;
        }

        /// <summary>
        /// Splits a given string into tokens
        /// </summary>
        /// <param name="exp">The string to be splitted</param>
        /// <returns>List of tokens</returns>
        public static List<string> SplitToTokens(string exp)
        {
            exp = UnaryToBin(exp);
            List<string> tokens = new List<string>();

            int len = exp.Length;
            int i = 0;

            while (i < len)
            {
                string token = "";

                if (char.IsDigit(exp[i]))
                {
                    while (i < len && (char.IsDigit(exp[i]) || exp[i] == '.' || exp[i] == 'e'))
                    {
                        token += exp[i];

                        if (exp[i] == 'e' && unary_op.Contains(exp[i + 1]))
                            token += exp[++i];
                        i++;
                    }
                }
                else if ((operators + "!").Contains(exp[i]))
                {
                    token += exp[i];
                    i++;
                }
                else if (char.IsLetter(exp[i]))
                {
                    while (i < len && char.IsLetter(exp[i]))
                    {
                        token += exp[i];
                        i++;
                    }
                }

                else if (exp[i] == '(' || exp[i] == ')' || exp[i] == ',')
                {
                    token += exp[i];
                    i++;
                }
                else
                    i++;

                if (token.Any())
                    tokens.Add(token);
            }

            return tokens;
        }
    }
}
