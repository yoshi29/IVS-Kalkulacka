using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionProcessor
{
    public class ExpressionProcessor
    {
        public static string Process(string expression)
        {
            List<string> tokens = Splitter.SplitToTokens(expression);
            string result = "";

            if (!Validator.IsValid(expression))
            {
                return "Error";
            }

            try
            {
                result = Solver.Solve(ShuntingYard.ToPostfix(tokens));
            } catch (Exception)
            {
                return "Error";
            }
           
            return result;
        }
    }
}
