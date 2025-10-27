using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TableManager.App.Controllers;
using ValueType = TableManager.App.Controllers.ValueType;

namespace TableManager.Tests
{

    public class EvaluatorTests
    {
        [TestMethod]
        [DataRow("=(5+3)", 8.0)]
        [DataRow("=(10-4)", 6.0)]
        [DataRow("=(3*4)", 12.0)]
        [DataRow("=(20/4)", 5.0)]
        [DataRow("=(17 mod 5)", 2.0)]
        [DataRow("=(20 div 3)", 6.0)]
        [DataRow("=((5+3)*2)", 16.0)]
        public void Evaluate_ArithmeticExpressions_ReturnsCorrectValue(string expression, double expected)
        {
            var cellValues = new Dictionary<string, string>();
            var result = Evaluator.Evaluate(expression, cellValues);

            Assert.IsTrue(result.Success, $"Обчислення '{expression}' має бути успішним");
            Assert.AreEqual(ValueType.Number, result.Type);
            Assert.AreEqual(expected, result.NumberValue, 0.0001, $"Результат '{expression}' має дорівнювати {expected}");
        }

        [TestMethod]
        [DataRow("=(inc(5))", 6.0)]
        [DataRow("=(dec(5))", 4.0)]
        [DataRow("=(inc(inc(5)))", 7.0)]
        [DataRow("=(dec(dec(5)))", 3.0)]
        [DataRow("=(inc(dec(10)))", 10.0)]
        public void Evaluate_IncDecFunctions_ReturnsCorrectValue(string expression, double expected)
        {
            var cellValues = new Dictionary<string, string>();
            var result = Evaluator.Evaluate(expression, cellValues);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(ValueType.Number, result.Type);
            Assert.AreEqual(expected, result.NumberValue, 0.0001);
        }

        [TestMethod]
        public void Evaluate_CellReferences_ReturnsCorrectValue()
        {
            var cellValues = new Dictionary<string, string>
            {
                ["A1"] = "10",
                ["A2"] = "5",
                ["A3"] = "=(A1+A2)"
            };
            var result = Evaluator.Evaluate("=(A3*2)", cellValues);

            Assert.IsTrue(result.Success, "Обчислення має бути успішним");
            Assert.AreEqual(ValueType.Number, result.Type);
            Assert.AreEqual(30.0, result.NumberValue, 0.0001, "(10+5)*2 повинно дорівнювати 30");
        }

        [TestMethod]
        [DataRow("=(10>5)", true)]
        [DataRow("=(5<10)", true)]
        [DataRow("=(10>=10)", true)]
        [DataRow("=(5<=10)", true)]
        [DataRow("=(10=10)", true)]
        [DataRow("=(10<>5)", true)]
        public void Evaluate_ComparisonTrue_ReturnsTrue(string expression, bool expected)
        {
            var cellValues = new Dictionary<string, string>();
            var result = Evaluator.Evaluate(expression, cellValues);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(ValueType.Boolean, result.Type);
            Assert.AreEqual(expected, result.BoolValue, $"{expression} має повернути {expected}");
        }

        [TestMethod]
        [DataRow("=(5>10)", false)]
        [DataRow("=(10<5)", false)]
        [DataRow("=(5>=10)", false)]
        [DataRow("=(10<=5)", false)]
        [DataRow("=(10=5)", false)]
        [DataRow("=(10<>10)", false)]
        public void Evaluate_ComparisonFalse_ReturnsFalse(string expression, bool expected)
        {
            var cellValues = new Dictionary<string, string>();
            var result = Evaluator.Evaluate(expression, cellValues);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(ValueType.Boolean, result.Type);
            Assert.AreEqual(expected, result.BoolValue);
        }

        [TestMethod]
        public void Evaluate_DivisionByZero_ReturnsError()
        {
            var cellValues = new Dictionary<string, string>();
            var result = Evaluator.Evaluate("=(10/0)", cellValues);

            Assert.IsFalse(result.Success, "Ділення на нуль має повернути помилку");
            Assert.AreEqual(ValueType.Error, result.Type);
            Assert.IsTrue(result.Error.StartsWith("#"), "Помилка має починатися з #");
        }

        [TestMethod]
        public void Evaluate_DivByZero_ReturnsError()
        {
            var cellValues = new Dictionary<string, string>();
            var result = Evaluator.Evaluate("=(10 div 0)", cellValues);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ValueType.Error, result.Type);
        }

        [TestMethod]
        public void Evaluate_InvalidSyntax_ReturnsError()
        {
            var cellValues = new Dictionary<string, string>
            {
                ["A1"] = "10"
            };

            var result = Evaluator.Evaluate("=(A1+)", cellValues);

            Assert.IsFalse(result.Success, "Невалідний вираз має повернути помилку");
            Assert.AreEqual(ValueType.Error, result.Type);
        }

        [TestMethod]
        public void Evaluate_NonExistentCell_ReturnsRefError()
        {
            var cellValues = new Dictionary<string, string>
            {
                ["A1"] = "10"
            };

            var result = Evaluator.Evaluate("=(XYZ999+5)", cellValues);

            Assert.IsFalse(result.Success, "Посилання на неіснуючу клітинку має повернути #REF");
            Assert.AreEqual("#REF", result.Error);
        }

        [TestMethod]
        public void DetectCycle_SimpleCycle_ReturnsTrue()
        {
            var allCells = new Dictionary<string, string>
            {
                ["A1"] = "=(A2+1)",
                ["A2"] = "=(A1+1)" 
            };
            var hasCycle = Evaluator.DetectCycle("A1", allCells);

            Assert.IsTrue(hasCycle, "Має виявити циклічне посилання A1 -> A2 -> A1");
        }

        [TestMethod]
        public void DetectCycle_NoCycle_ReturnsFalse()
        {
            var allCells = new Dictionary<string, string>
            {
                ["A1"] = "10",
                ["A2"] = "=(A1+5)",
                ["A3"] = "=(A2*2)"
            };

            var hasCycle = Evaluator.DetectCycle("A3", allCells);

            Assert.IsFalse(hasCycle, "Не повинно бути циклу в ланцюжку A1 -> A2 -> A3");
        }

        [TestMethod]
        public void DetectCycle_SelfReference_ReturnsTrue()
        {
            var allCells = new Dictionary<string, string>
            {
                ["A1"] = "=(A1+1)" 
            };

            var hasCycle = Evaluator.DetectCycle("A1", allCells);

            Assert.IsTrue(hasCycle, "Має виявити самопосилання A1 -> A1");
        }

        [TestMethod]
        public void DetectCycle_ComplexCycle_ReturnsTrue()
        {
            var allCells = new Dictionary<string, string>
            {
                ["A1"] = "=(A2+1)",
                ["A2"] = "=(A3+1)",
                ["A3"] = "=(A1+1)" 
            };

            var hasCycle = Evaluator.DetectCycle("A1", allCells);

            Assert.IsTrue(hasCycle, "Має виявити цикл A1 -> A2 -> A3 -> A1");
        }

        [TestMethod]
        public void Evaluate_EmptyCell_ReturnsFalse()
        {
            var cellValues = new Dictionary<string, string>
            {
                ["A1"] = ""
            };
            var result = Evaluator.Evaluate("=(A1+5)", cellValues);

            Assert.IsTrue(result.Success, "Порожня клітинка має розглядатися як 0");
            Assert.AreEqual(5.0, result.NumberValue, 0.0001, "Порожня + 5 = 5");
        }
    }
}