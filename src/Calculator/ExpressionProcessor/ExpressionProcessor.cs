using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionProcessor
{
    public class ExpressionProcessor
    {
        public static string Process(string expression)
        {
            string exp = expression.ToLower();
            List<string> tokens = Splitter.SplitToTokens(exp);
            string result = "";

            if (!Validator.IsValid(exp))
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
