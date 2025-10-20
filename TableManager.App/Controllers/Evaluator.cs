using System;
using System.Collections.Generic;
using System.Linq;
using TableManager.App.Models;

namespace TableManager.App.Controllers
{
    public class Evaluator
    {
        // Обчислення виразу
        public static (bool success, double result, string error) Evaluate(string expression, Dictionary<string, string>? cellValues = null)
        {
            try
            {
                // Якщо не формула, спробуємо парсити як число
                if (!expression.StartsWith("="))
                {
                    if (double.TryParse(expression, out double constValue))
                        return (true, constValue, "");
                    else
                        return (false, 0, "Невалідна константа");
                }

                string expr = expression.Substring(1).Trim();

                // Підставляємо значення клітинок
                if (cellValues != null)
                {
                    expr = SubstituteCellReferences(expr, cellValues);
                }

                // Перевірка синтаксису
                var syntaxCheck = Parser.CheckSyntax("=" + expr);
                if (!syntaxCheck.isValid)
                    return (false, 0, syntaxCheck.error);

                // Обчислюємо
                double result = EvaluateExpression(expr);
                return (true, result, "");
            }
            catch (Exception ex)
            {
                return (false, 0, $"Помилка обчислення: {ex.Message}");
            }
        }

        // Підстановка значень клітинок
        private static string SubstituteCellReferences(string expr, Dictionary<string, string> cellValues)
        {
            var dependencies = Parser.GetDependencies("=" + expr);
            
            foreach (var dep in dependencies)
            {
                if (cellValues.ContainsKey(dep))
                {
                    string cellValue = cellValues[dep];
                    
                    // Якщо клітинка містить формулу, обчислюємо її
                    if (cellValue.StartsWith("="))
                    {
                        var evalResult = Evaluate(cellValue, cellValues);
                        if (evalResult.success)
                            expr = expr.Replace(dep, evalResult.result.ToString());
                        else
                            throw new Exception($"#REF: {dep}");
                    }
                    else if (double.TryParse(cellValue, out double value))
                    {
                        expr = expr.Replace(dep, value.ToString());
                    }
                    else
                    {
                        throw new Exception($"#REF: {dep}");
                    }
                }
                else
                {
                    throw new Exception($"#REF: {dep}");
                }
            }

            return expr;
        }

        // Базове обчислення виразу
        private static double EvaluateExpression(string expr)
        {
            // Обробка функцій
            expr = ProcessFunctions(expr);

            // Простий калькулятор для базових операцій
            return SimpleCalculator(expr);
        }

        // Обробка функцій (inc, dec)
        private static string ProcessFunctions(string expr)
        {
            // inc(x)
            while (expr.Contains("inc("))
            {
                int start = expr.IndexOf("inc(");
                int end = FindClosingBracket(expr, start + 4);
                string arg = expr.Substring(start + 4, end - start - 4);
                double value = SimpleCalculator(arg);
                expr = expr.Remove(start, end - start + 1).Insert(start, (value + 1).ToString());
            }

            // dec(x)
            while (expr.Contains("dec("))
            {
                int start = expr.IndexOf("dec(");
                int end = FindClosingBracket(expr, start + 4);
                string arg = expr.Substring(start + 4, end - start - 4);
                double value = SimpleCalculator(arg);
                expr = expr.Remove(start, end - start + 1).Insert(start, (value - 1).ToString());
            }

            return expr;
        }

        // Знаходження закриваючої дужки
        private static int FindClosingBracket(string expr, int start)
        {
            int count = 1;
            for (int i = start; i < expr.Length; i++)
            {
                if (expr[i] == '(') count++;
                if (expr[i] == ')') count--;
                if (count == 0) return i;
            }
            return -1;
        }

        // Простий калькулятор (базові операції)
        private static double SimpleCalculator(string expr)
        {
            expr = expr.Trim();

            // Використовуємо DataTable.Compute для обчислення
            try
            {
                // Заміна mod та div
                expr = expr.Replace(" mod ", " % ");
                expr = expr.Replace(" div ", " / ");

                var table = new System.Data.DataTable();
                var result = table.Compute(expr, "");
                return Convert.ToDouble(result);
            }
            catch
            {
                throw new Exception("#ERROR");
            }
        }

        // Перевірка циклічних залежностей
        public static bool DetectCycle(string cellName, Dictionary<string, string> allCells, HashSet<string>? visited = null)
        {
            if (visited == null)
                visited = new HashSet<string>();

            if (visited.Contains(cellName))
                return true; // Цикл знайдено

            visited.Add(cellName);

            if (!allCells.ContainsKey(cellName))
                return false;

            var dependencies = Parser.GetDependencies(allCells[cellName]);

            foreach (var dep in dependencies)
            {
                if (DetectCycle(dep, allCells, new HashSet<string>(visited)))
                    return true;
            }

            return false;
        }
    }
}