using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace ExpressionProcessor
{
    public class ShuntingYard
    {
        private static Dictionary<string, (string symbol, int precedence, bool rightAssociative)> operators =
        new (string symbol, int precedence, bool rightAssociative)[] {
            ("!", 5, false),
            ("^", 4, true),
            ("×", 3, false),
            ("÷", 3, false),
            ("+", 2, false),
            ("−", 2, false)
        }.ToDictionary(op => op.symbol);

        private static List<string> functions = new List<string> { "abs", "root" };

        /// <summary>
        /// Convert mathematical expression to postfix notation
        /// </summary>
        /// <param name="tokens">List of tokens of mathematical expression</param>
        /// <returns>List of tokens in postfix notation</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="tokens"/> contains unclosed expression</exception>
        public static List<string> ToPostfix(List<string> tokens)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");

            List<string> output = new List<string>();
            Stack<string> stack = new Stack<string>();
            //CultureInfo culture = CultureInfo.InvariantCulture;
            //NumberStyles style = NumberStyles.Float;

            foreach (string token in tokens)
            {
                if (Double.TryParse(token, out _)) //token is number
                {
                    output.Add(token);
                }

                else if (functions.Contains(token)) //token is function
                {
                    stack.Push(token);
                }

                else if (token == ",") //token is separator of arguments of function
                {
                    while (stack.Count != 0 && stack.Peek() != "(") //searching for left parenthesis on stack 
                    {
                        output.Add(stack.Peek());
                        stack.Pop();

                        if (stack.Count == 0) //left parenthesis was not found on stack
                            throw new ArgumentException("Wrong format of function");
                    }
                }

                else if (operators.TryGetValue(token, out var op1)) //token (op1) is opearator
                {
                    while (stack.Count != 0 && operators.TryGetValue(stack.Peek(), out var op2)) //searching for operator on stack
                    {
                        if ((!op1.rightAssociative && op1.precedence <= op2.precedence) ||
                            (op1.rightAssociative && op1.precedence < op2.precedence))
                        {
                            output.Add(op2.symbol);
                            stack.Pop();
                        }
                        else break;
                    }
                    stack.Push(token);
                }

                else if (token == "(") //token is left parenthesis
                {
                    stack.Push(token);
                }

                else if (token == ")") //token is right parenthesis
                {
                    bool closed = false;
                    while (stack.Count != 0 && stack.Peek() != "(") //moving operators from stack to output
                    {
                        output.Add(stack.Peek());
                        stack.Pop();
                    }
                    if (stack.Count != 0 && stack.Peek() == "(")
                    {
                        closed = true;
                        stack.Pop();
                    }
                    if (stack.Count != 0 && functions.Contains(stack.Peek())) //token is function
                    {
                        output.Add(stack.Peek());
                        stack.Pop();
                    }
                    else if (stack.Count == 0 && !closed) //left parenthesis was not found on stack
                    {
                        throw new ArgumentException("Unclosed expression");
                    }
                }
            } //foreach end

            while (stack.Count != 0) //moving operators from stack to output
            {
                if (stack.Peek() == "(" || stack.Peek() == ")") //unclosed expression
                {
                    throw new ArgumentException("Unclosed expression");
                }
                output.Add(stack.Peek());
                stack.Pop();
            }

            return output;
        } //ToPostfix() end
    }
}