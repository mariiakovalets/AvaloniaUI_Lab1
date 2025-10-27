using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TableManager.App.Controllers;

namespace TableManager.Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        [DataRow("=(5+3)", true)]
        [DataRow("=((A1+A2)*2)", true)]
        [DataRow("=(inc(A1)+dec(B1))", true)]
        [DataRow("=(A1>=A2)", true)]
        [DataRow("=(A1 mod B1)", true)]
        [DataRow("=(A1 div B2)", true)]
        public void CheckSyntax_ValidExpressions_ReturnsTrue(string expression, bool expectedValid)
        {
            var result = Parser.CheckSyntax(expression);
            Assert.AreEqual(expectedValid, result.isValid, $"Вираз '{expression}' має бути валідним");
        }

        [TestMethod]
        [DataRow("=(A1+)", false)]
        [DataRow("=((A1+A2)", false)]
        [DataRow("=(A1  +A2)", false)]
        [DataRow("=(+A1)", false)]
        [DataRow("=(A1++A2)", false)]
        public void CheckSyntax_InvalidExpressions_ReturnsFalse(string expression, bool expectedValid)
        {
            var result = Parser.CheckSyntax(expression);

            Assert.AreEqual(expectedValid, result.isValid, $"Вираз '{expression}' має бути НЕвалідним");
        }

        [TestMethod]
        public void GetDependencies_SimpleExpression_FindsAllReferences()
        {
            string expression = "=(A1+B2)";
            var expected = new List<string> { "A1", "B2" };
            var dependencies = Parser.GetDependencies(expression);

            Assert.AreEqual(expected.Count, dependencies.Count, "Кількість залежностей не співпадає");
            CollectionAssert.AreEquivalent(expected, dependencies, "Залежності не співпадають");
        }

        [TestMethod]
        public void GetDependencies_ComplexExpression_FindsAllReferences()
        {
            string expression = "=(inc(A1)+dec(B1))";
            var expected = new List<string> { "A1", "B1" };
            var dependencies = Parser.GetDependencies(expression);

            Assert.AreEqual(expected.Count, dependencies.Count);
            CollectionAssert.AreEquivalent(expected, dependencies);
        }

        [TestMethod]
        public void GetDependencies_MultipleReferences_FindsAllUnique()
        {
            string expression = "=((A1+A2)*B3)";
            var expected = new List<string> { "A1", "A2", "B3" };
            var dependencies = Parser.GetDependencies(expression);

            Assert.AreEqual(expected.Count, dependencies.Count);
            CollectionAssert.AreEquivalent(expected, dependencies);
        }

        [TestMethod]
        public void GetDependencies_NoReferences_ReturnsEmpty()
        {
            string expression = "=(5+3)";
            var dependencies = Parser.GetDependencies(expression);

            Assert.AreEqual(0, dependencies.Count, "Не повинно бути жодних залежностей");
        }

        [TestMethod]
        public void GetDependencies_ComparisonExpression_FindsReferences()
        {
            string expression = "=(C5>=D10)";
            var expected = new List<string> { "C5", "D10" };
            var dependencies = Parser.GetDependencies(expression);

            CollectionAssert.AreEquivalent(expected, dependencies);
        }
    }
}