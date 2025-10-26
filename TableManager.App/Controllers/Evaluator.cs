
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TableManager.App.Controllers
{
    public enum ValueType
    {
        Number,
        Boolean,
        Error
    }

    public class EvaluationResult
    {
        public bool Success { get; set; }
        public ValueType Type { get; set; }
        public double NumberValue { get; set; }
        public bool BoolValue { get; set; }
        public string Error { get; set; } = "";
        
        public string GetDisplayValue()
        {
            if (!Success) return Error;
            
            if (Type == ValueType.Number)
            {
                if (NumberValue == Math.Floor(NumberValue))
                    return ((int)NumberValue).ToString();
                return NumberValue.ToString();
            }
            
            if (Type == ValueType.Boolean)
                return BoolValue ? "TRUE" : "FALSE";
            
            return Error;
        }
    }

    public static class Evaluator
    {
        public static EvaluationResult Evaluate(string expression, Dictionary<string, string> cellValues)
        {
            if (string.IsNullOrWhiteSpace(expression))
                return new EvaluationResult { Success = false, Type = ValueType.Error, Error = "#ERROR" };

            if (!expression.StartsWith("="))
                return new EvaluationResult { Success = false, Type = ValueType.Error, Error = "#ERROR" };

            string expr = expression.Substring(1).Trim();

            var syntaxCheck = Parser.CheckSyntax(expression);
            if (!syntaxCheck.isValid)
                return new EvaluationResult { Success = false, Type = ValueType.Error, Error = "#ERROR" };

            if (expr.StartsWith("(") && expr.EndsWith(")"))
            {
                expr = expr.Substring(1, expr.Length - 2).Trim();
            }

            try
            {
                return EvaluateExpression(expr, cellValues);
            }
            catch (Exception)
            {
                return new EvaluationResult { Success = false, Type = ValueType.Error, Error = "#ERROR" };
            }
        }

        private static EvaluationResult EvaluateExpression(string expr, Dictionary<string, string> cellValues)
        {
            if (expr.Contains(">="))
            {
                var parts = expr.Split(new[] { ">=" }, StringSplitOptions.None);
                if (parts.Length == 2)
                {
                    var left = EvaluateExpression(parts[0].Trim(), cellValues);
                    var right = EvaluateExpression(parts[1].Trim(), cellValues);
                    
                    if (!left.Success) return left;
                    if (!right.Success) return right;
                    
                    if (left.Type != ValueType.Number || right.Type != ValueType.Number)
                        return new EvaluationResult { Success = false, Type = ValueType.Error, Error = "#ERROR" };
                    
                    return new EvaluationResult { Success = true, Type = ValueType.Boolean, BoolValue = left.NumberValue >= right.NumberValue };
                }
            }
            
            if (expr.Contains("<="))
            {
                var parts = expr.Split(new[] { "<=" }, StringSplitOptions.None);
                if (parts.Length == 2)
                {
                    var left = EvaluateExpression(parts[0].Trim(), cellValues);
                    var right = EvaluateExpression(parts[1].Trim(), cellValues);
                    
                    if (!left.Success) return left;
                    if (!right.Success) return right;
                    
                    if (left.Type != ValueType.Number || right.Type != ValueType.Number)
                        return new EvaluationResult { Success = false, Type = ValueType.Error, Error = "#ERROR" };
                    
                    return new EvaluationResult { Success = true, Type = ValueType.Boolean, BoolValue = left.NumberValue <= right.NumberValue };
                }
            }
            
            if (expr.Contains("<>"))
            {
                var parts = expr.Split(new[] { "<>" }, StringSplitOptions.None);
                if (parts.Length == 2)
                {
                    var left = EvaluateExpression(parts[0].Trim(), cellValues);
                    var right = EvaluateExpression(parts[1].Trim(), cellValues);
                    
                    if (!left.Success) return left;
                    if (!right.Success) return right;
                    
                    if (left.Type != ValueType.Number || right.Type != ValueType.Number)
                        return new EvaluationResult { Success = false, Type = ValueType.Error, Error = "#ERROR" };
                    
                    return new EvaluationResult { Success = true, Type = ValueType.Boolean, BoolValue = Math.Abs(left.NumberValue - right.NumberValue) > 0.0001 };
                }
            }
            
            if (expr.Contains(">") && !expr.Contains(">="))
            {
                var parts = expr.Split('>');
                if (parts.Length == 2)
                {
                    var left = EvaluateExpression(parts[0].Trim(), cellValues);
                    var right = EvaluateExpression(parts[1].Trim(), cellValues);
                    
                    if (!left.Success) return left;
                    if (!right.Success) return right;
                    
                    if (left.Type != ValueType.Number || right.Type != ValueType.Number)
                        return new EvaluationResult { Success = false, Type = ValueType.Error, Error = "#ERROR" };
                    
                    return new EvaluationResult { Success = true, Type = ValueType.Boolean, BoolValue = left.NumberValue > right.NumberValue };
                }
            }
            
            if (expr.Contains("<") && !expr.Contains("<=") && !expr.Contains("<>"))
            {
                var parts = expr.Split('<');
                if (parts.Length == 2)
                {
                    var left = EvaluateExpression(parts[0].Trim(), cellValues);
                    var right = EvaluateExpression(parts[1].Trim(), cellValues);
                    
                    if (!left.Success) return left;
                    if (!right.Success) return right;
                    
                    if (left.Type != ValueType.Number || right.Type != ValueType.Number)
                        return new EvaluationResult { Success = false, Type = ValueType.Error, Error = "#ERROR" };
                    
                    return new EvaluationResult { Success = true, Type = ValueType.Boolean, BoolValue = left.NumberValue < right.NumberValue };
                }
            }
            
            if (expr.Contains("=") && !expr.Contains("<=") && !expr.Contains(">="))
            {
                var parts = expr.Split('=');
                if (parts.Length == 2)
                {
                    var left = EvaluateExpression(parts[0].Trim(), cellValues);
                    var right = EvaluateExpression(parts[1].Trim(), cellValues);
                    
                    if (!left.Success) return left;
                    if (!right.Success) return right;
                    
                    if (left.Type != ValueType.Number || right.Type != ValueType.Number)
                        return new EvaluationResult { Success = false, Type = ValueType.Error, Error = "#ERROR" };
                    
                    return new EvaluationResult { Success = true, Type = ValueType.Boolean, BoolValue = Math.Abs(left.NumberValue - right.NumberValue) < 0.0001 };
                }
            }

            return EvaluateArithmetic(expr, cellValues);
        }

        private static EvaluationResult EvaluateArithmetic(string expr, Dictionary<string, string> cellValues)
        {
            if (expr.StartsWith("inc("))
            {
                int closingParen = FindMatchingParen(expr, 3);
                if (closingParen == expr.Length - 1)
                {
                    string inner = expr.Substring(4, closingParen - 4).Trim();
                    var result = EvaluateArithmetic(inner, cellValues);
                    if (!result.Success) return result;
                    if (result.Type != ValueType.Number)
                        return new EvaluationResult { Success = false, Type = ValueType.Error, Error = "#ERROR" };
                    
                    return new EvaluationResult { Success = true, Type = ValueType.Number, NumberValue = result.NumberValue + 1 };
                }
            }
            
            if (expr.StartsWith("dec("))
            {
                int closingParen = FindMatchingParen(expr, 3);
                if (closingParen == expr.Length - 1)
                {
                    string inner = expr.Substring(4, closingParen - 4).Trim();
                    var result = EvaluateArithmetic(inner, cellValues);
                    if (!result.Success) return result;
                    if (result.Type != ValueType.Number)
                        return new EvaluationResult { Success = false, Type = ValueType.Error, Error = "#ERROR" };
                    
                    return new EvaluationResult { Success = true, Type = ValueType.Number, NumberValue = result.NumberValue - 1 };
                }
            }

            int parenLevel = 0;
            
            for (int i = expr.Length - 1; i >= 0; i--)
            {
                if (expr[i] == ')') parenLevel++;
                if (expr[i] == '(') parenLevel--;
                
                if (parenLevel == 0 && (expr[i] == '+' || (expr[i] == '-' && i > 0)))
                {
                    var left = EvaluateArithmetic(expr.Substring(0, i).Trim(), cellValues);
                    var right = EvaluateArithmetic(expr.Substring(i + 1).Trim(), cellValues);
                    
                    if (!left.Success) return left;
                    if (!right.Success) return right;
                    
                    if (left.Type != ValueType.Number || right.Type != ValueType.Number)
                        return new EvaluationResult { Success = false, Type = ValueType.Error, Error = "#ERROR" };
                    
                    double result = expr[i] == '+' ? left.NumberValue + right.NumberValue : left.NumberValue - right.NumberValue;
                    return new EvaluationResult { Success = true, Type = ValueType.Number, NumberValue = result };
                }
            }
            
            parenLevel = 0;
            
            for (int i = expr.Length - 1; i >= 0; i--)
            {
                if (expr[i] == ')') parenLevel++;
                if (expr[i] == '(') parenLevel--;
                
                if (parenLevel == 0)
                {
                    if (expr[i] == '*' || expr[i] == '/')
                    {
                        var left = EvaluateArithmetic(expr.Substring(0, i).Trim(), cellValues);
                        var right = EvaluateArithmetic(expr.Substring(i + 1).Trim(), cellValues);
                        
                        if (!left.Success) return left;
                        if (!right.Success) return right;
                        
                        if (left.Type != ValueType.Number || right.Type != ValueType.Number)
                            return new EvaluationResult { Success = false, Type = ValueType.Error, Error = "#ERROR" };
                        
                        if (expr[i] == '/' && Math.Abs(right.NumberValue) < 0.0001)
                            return new EvaluationResult { Success = false, Type = ValueType.Error, Error = "#ERROR" };
                        
                        double result = expr[i] == '*' ? left.NumberValue * right.NumberValue : left.NumberValue / right.NumberValue;
                        return new EvaluationResult { Success = true, Type = ValueType.Number, NumberValue = result };
                    }
                    
                    if (i >= 3 && expr.Substring(i - 3, 4) == " mod")
                    {
                        var left = EvaluateArithmetic(expr.Substring(0, i - 3).Trim(), cellValues);
                        var right = EvaluateArithmetic(expr.Substring(i + 1).Trim(), cellValues);
                        
                        if (!left.Success) return left;
                        if (!right.Success) return right;
                        
                        if (left.Type != ValueType.Number || right.Type != ValueType.Number)
                            return new EvaluationResult { Success = false, Type = ValueType.Error, Error = "#ERROR" };
                        
                        return new EvaluationResult { Success = true, Type = ValueType.Number, NumberValue = (int)left.NumberValue % (int)right.NumberValue };
                    }
                    
                    if (i >= 3 && expr.Substring(i - 3, 4) == " div")
                    {
                        var left = EvaluateArithmetic(expr.Substring(0, i - 3).Trim(), cellValues);
                        var right = EvaluateArithmetic(expr.Substring(i + 1).Trim(), cellValues);
                        
                        if (!left.Success) return left;
                        if (!right.Success) return right;
                        
                        if (left.Type != ValueType.Number || right.Type != ValueType.Number)
                            return new EvaluationResult { Success = false, Type = ValueType.Error, Error = "#ERROR" };
                        
                        if (Math.Abs(right.NumberValue) < 0.0001)
                            return new EvaluationResult { Success = false, Type = ValueType.Error, Error = "#ERROR" };
                        
                        return new EvaluationResult { Success = true, Type = ValueType.Number, NumberValue = (int)left.NumberValue / (int)right.NumberValue };
                    }
                }
            }

            // Дужки
            if (expr.StartsWith("(") && expr.EndsWith(")"))
            {
                return EvaluateArithmetic(expr.Substring(1, expr.Length - 2).Trim(), cellValues);
            }

            if (Regex.IsMatch(expr, @"^[A-Z]+\d+$"))
            {
                if (!cellValues.ContainsKey(expr))
                    return new EvaluationResult { Success = false, Type = ValueType.Error, Error = "#REF" };
                
                string cellValue = cellValues[expr];
                
                if (string.IsNullOrWhiteSpace(cellValue))
                    return new EvaluationResult { Success = true, Type = ValueType.Number, NumberValue = 0 };
                
                if (cellValue.StartsWith("="))
                    return Evaluate(cellValue, cellValues);
                
                if (double.TryParse(cellValue, out double num))
                    return new EvaluationResult { Success = true, Type = ValueType.Number, NumberValue = num };
                
                return new EvaluationResult { Success = false, Type = ValueType.Error, Error = "#ERROR" };
            }

            if (double.TryParse(expr, out double value))
            {
                return new EvaluationResult { Success = true, Type = ValueType.Number, NumberValue = value };
            }

            return new EvaluationResult { Success = false, Type = ValueType.Error, Error = "#ERROR" };
        }

        private static int FindMatchingParen(string expr, int openParenIndex)
        {
            int level = 1;
            for (int i = openParenIndex + 1; i < expr.Length; i++)
            {
                if (expr[i] == '(') level++;
                if (expr[i] == ')') level--;
                if (level == 0) return i;
            }
            return -1;
        }

        public static bool DetectCycle(string cellName, Dictionary<string, string> allCells)
        {
            var visited = new HashSet<string>();
            return DetectCycleHelper(cellName, allCells, visited, new HashSet<string>());
        }

        private static bool DetectCycleHelper(string current, Dictionary<string, string> allCells, HashSet<string> visited, HashSet<string> recStack)
        {
            if (!allCells.ContainsKey(current))
                return false;

            if (recStack.Contains(current))
                return true;

            if (visited.Contains(current))
                return false;

            visited.Add(current);
            recStack.Add(current);

            string expr = allCells[current];
            if (expr.StartsWith("="))
            {
                var dependencies = Parser.GetDependencies(expr);
                foreach (var dep in dependencies)
                {
                    if (DetectCycleHelper(dep, allCells, visited, recStack))
                        return true;
                }
            }

            recStack.Remove(current);
            return false;
        }
    }
}