using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TableManager.App.Controllers
{
    public class Parser
    {
      
        public static (bool isValid, string error) CheckSyntax(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                return (false, "Порожній вираз");

          
            if (!expression.StartsWith("="))
                return (true, "");

           
            string expr = expression.Substring(1).Trim();

            if (string.IsNullOrWhiteSpace(expr))
                return (false, "Порожній вираз після =");

            
            if (expr.Contains("  "))
                return (false, "Подвійні пробіли не дозволені");

       
            if (!CheckBrackets(expr))
                return (false, "Неправильна розстановка дужок");

           
            if (!CheckOperators(expr))
                return (false, "Неправильне використання операторів");

            return (true, "");
        }

       
        private static bool CheckBrackets(string expr)
        {
            int count = 0;
            foreach (char c in expr)
            {
                if (c == '(') count++;
                if (c == ')') count--;
                if (count < 0) return false;
            }
            return count == 0;
        }

        
        private static bool CheckOperators(string expr)
        {
            string[] invalidPatterns = { "++", "+-", "+*", "+/", "--", "-*", "-/", "**", "*/", "//" };
            foreach (var pattern in invalidPatterns)
            {
                if (expr.Contains(pattern))
                    return false;
            }

            
            char[] operators = { '+', '-', '*', '/' };
            if (operators.Contains(expr.TrimStart()[0]) && expr.TrimStart()[0] != '-')
                return false;
            if (operators.Contains(expr.TrimEnd()[^1]))
                return false;

            return true;
        }

   
        public static List<string> Parse(string expression)
        {
            var tokens = new List<string>();

            if (string.IsNullOrWhiteSpace(expression))
                return tokens;

           
            if (!expression.StartsWith("="))
            {
                tokens.Add(expression);
                return tokens;
            }

            string expr = expression.Substring(1).Trim();

         
            var regex = new Regex(@"(\d+\.?\d*|[A-Z]+\d+|inc|dec|mod|div|not|and|or|eqv|min|max|mmin|mmax|sum|[+\-*/()=<>]|<=|>=|<>)");
            var matches = regex.Matches(expr);

            foreach (Match match in matches)
            {
                tokens.Add(match.Value);
            }

            return tokens;
        }

      
        public static List<string> GetDependencies(string expression)
        {
            var dependencies = new List<string>();

            if (!expression.StartsWith("="))
                return dependencies;

           
            var regex = new Regex(@"[A-Z]+\d+");
            var matches = regex.Matches(expression);

            foreach (Match match in matches)
            {
                if (!dependencies.Contains(match.Value))
                    dependencies.Add(match.Value);
            }

            return dependencies;
        }
    }
}