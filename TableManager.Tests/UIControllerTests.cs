using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TableManager.App.Controllers;
using TableManager.App.Views;

namespace TableManager.Tests
{

    [TestClass]
    public class UIControllerTests
    {
        private UIController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            _controller = new UIController();
        }

        [TestMethod]
        public void CalculateCell_SimpleNumber_SetsDisplayValue()
        {

            var cell = new CellViewModel
            {
                Value = "42",
                CellName = "A1"
            };
            var cellMap = new Dictionary<string, CellViewModel>
            {
                ["A1"] = cell
            };

            _controller.CalculateCell(cell, cellMap);

            Assert.AreEqual("42", cell.DisplayValue, "DisplayValue має дорівнювати '42'");
            Assert.IsFalse(cell.HasError, "Не повинно бути помилки");
        }

        [TestMethod]
        public void CalculateCell_ValidFormula_CalculatesCorrectly()
        {
            var cellA1 = new CellViewModel { Value = "10", CellName = "A1" };
            var cellA2 = new CellViewModel { Value = "5", CellName = "A2" };
            var cellA3 = new CellViewModel { Value = "=(A1+A2)", CellName = "A3" };
            
            var cellMap = new Dictionary<string, CellViewModel>
            {
                ["A1"] = cellA1,
                ["A2"] = cellA2,
                ["A3"] = cellA3
            };

            _controller.CalculateCell(cellA3, cellMap);

            Assert.AreEqual("15", cellA3.DisplayValue, "10 + 5 має дорівнювати 15");
            Assert.IsFalse(cellA3.HasError);
        }

        [TestMethod]
        public void CalculateCell_CyclicReference_SetsErrorMessage()
        {
            var cellA1 = new CellViewModel { Value = "=(A2+1)", CellName = "A1" };
            var cellA2 = new CellViewModel { Value = "=(A1+1)", CellName = "A2" };
            
            var cellMap = new Dictionary<string, CellViewModel>
            {
                ["A1"] = cellA1,
                ["A2"] = cellA2
            };

            _controller.CalculateCell(cellA1, cellMap);

            Assert.AreEqual("#CYCLE", cellA1.DisplayValue, "Має показати #CYCLE");
            Assert.IsTrue(cellA1.HasError, "Має бути помилка");
            Assert.AreEqual("Циклічне посилання", cellA1.ErrorMessage);
        }

        [TestMethod]
        public void CalculateCell_SyntaxError_SetsError()
        {
            var cell = new CellViewModel { Value = "=(5+)", CellName = "A1" };
            var cellMap = new Dictionary<string, CellViewModel>
            {
                ["A1"] = cell
            };

            _controller.CalculateCell(cell, cellMap);

            Assert.AreEqual("#ERROR", cell.DisplayValue);
            Assert.IsTrue(cell.HasError);
        }

        [TestMethod]
        public void RecalculateAll_CalculatesAllCells()
        {
            var cellA1 = new CellViewModel { Value = "10", CellName = "A1" };
            var cellA2 = new CellViewModel { Value = "=(A1+5)", CellName = "A2" };
            var cellA3 = new CellViewModel { Value = "=(A2*2)", CellName = "A3" };
            
            var row = new RowViewModel();
            row.Cells.Add(cellA1);
            row.Cells.Add(cellA2);
            row.Cells.Add(cellA3);
            
            var rows = new ObservableCollection<RowViewModel> { row };
            var cellMap = new Dictionary<string, CellViewModel>
            {
                ["A1"] = cellA1,
                ["A2"] = cellA2,
                ["A3"] = cellA3
            };
            _controller.RecalculateAll(rows, cellMap);

            Assert.AreEqual("10", cellA1.DisplayValue);
            Assert.AreEqual("15", cellA2.DisplayValue, "10 + 5 = 15");
            Assert.AreEqual("30", cellA3.DisplayValue, "15 * 2 = 30");
        }

        [TestMethod]
        public void RecalculateChanged_OnlyRecalculatesChangedCells()
        {
            var cellA1 = new CellViewModel { Value = "10", CellName = "A1" };
            var cellA2 = new CellViewModel { Value = "=(A1+5)", CellName = "A2" };
            var cellB1 = new CellViewModel { Value = "100", CellName = "B1" };
            
            var cellMap = new Dictionary<string, CellViewModel>
            {
                ["A1"] = cellA1,
                ["A2"] = cellA2,
                ["B1"] = cellB1
            };
            
            _controller.CalculateCell(cellA1, cellMap);
            _controller.CalculateCell(cellA2, cellMap);
            _controller.CalculateCell(cellB1, cellMap);
            
            Assert.AreEqual("15", cellA2.DisplayValue);
            cellA1.Value = "20";
            var changedCells = new HashSet<string> { "A1" };

            _controller.RecalculateChanged(changedCells, cellMap);

            Assert.AreEqual("20", cellA1.DisplayValue);
            Assert.AreEqual("25", cellA2.DisplayValue, "A2 має бути перераховано (залежить від A1)");
            Assert.AreEqual("100", cellB1.DisplayValue, "B1 НЕ має бути перераховано");
        }

        [TestMethod]
        public void FindAllDependents_FindsDirectDependency()
        {
            var cellA1 = new CellViewModel { Value = "10", CellName = "A1" };
            var cellA2 = new CellViewModel { Value = "=(A1+5)", CellName = "A2" };
            
            var cellMap = new Dictionary<string, CellViewModel>
            {
                ["A1"] = cellA1,
                ["A2"] = cellA2
            };
            
            var changedCells = new HashSet<string> { "A1" };
            var dependents = _controller.FindAllDependents(changedCells, cellMap);

            Assert.IsTrue(dependents.Contains("A2"), "A2 залежить від A1");
        }

        [TestMethod]
        public void FindAllDependents_FindsTransitiveDependencies()
        {
            var cellA1 = new CellViewModel { Value = "10", CellName = "A1" };
            var cellA2 = new CellViewModel { Value = "=(A1+5)", CellName = "A2" };
            var cellA3 = new CellViewModel { Value = "=(A2*2)", CellName = "A3" };
            var cellB1 = new CellViewModel { Value = "100", CellName = "B1" };
            
            var cellMap = new Dictionary<string, CellViewModel>
            {
                ["A1"] = cellA1,
                ["A2"] = cellA2,
                ["A3"] = cellA3,
                ["B1"] = cellB1
            };
            
            var changedCells = new HashSet<string> { "A1" };
            var dependents = _controller.FindAllDependents(changedCells, cellMap);

            Assert.IsTrue(dependents.Contains("A2"), "A2 залежить від A1");
            Assert.IsTrue(dependents.Contains("A3"), "A3 залежить від A2 (транзитивно від A1)");
            Assert.IsFalse(dependents.Contains("B1"), "B1 не залежить від A1");
        }

        [TestMethod]
        public void AddRow_AddsNewRowWithCorrectCells()
        {
            var rows = new ObservableCollection<RowViewModel>();
            var headers = new ObservableCollection<string> { "A", "B", "C" };
            var cellMap = new Dictionary<string, CellViewModel>();

            var row1 = new RowViewModel { RowNumber = "1" };
            for (int i = 0; i < 3; i++)
            {
                var cell = new CellViewModel { CellName = $"{headers[i]}1", Value = "" };
                row1.Cells.Add(cell);
                cellMap[cell.CellName] = cell;
            }
            rows.Add(row1);
            _controller.AddRow(rows, headers, cellMap, GetColumnName);

            Assert.AreEqual(2, rows.Count, "Має бути 2 рядки");
            Assert.AreEqual("2", rows[1].RowNumber, "Номер другого рядка має бути '2'");
            Assert.AreEqual(3, rows[1].Cells.Count, "У новому рядку має бути 3 клітинки");
            Assert.IsTrue(cellMap.ContainsKey("A2"), "Клітинка A2 має бути в cellMap");
            Assert.IsTrue(cellMap.ContainsKey("B2"), "Клітинка B2 має бути в cellMap");
            Assert.IsTrue(cellMap.ContainsKey("C2"), "Клітинка C2 має бути в cellMap");
        }

        [TestMethod]
        public void DeleteLastRow_RemovesRowAndCells()
        {
            var rows = new ObservableCollection<RowViewModel>();
            var cellMap = new Dictionary<string, CellViewModel>();
            
            for (int rowIdx = 1; rowIdx <= 2; rowIdx++)
            {
                var row = new RowViewModel { RowNumber = $"{rowIdx}" };
                for (int colIdx = 0; colIdx < 2; colIdx++)
                {
                    string cellName = $"{GetColumnName(colIdx)}{rowIdx}";
                    var cell = new CellViewModel { CellName = cellName, Value = "" };
                    row.Cells.Add(cell);
                    cellMap[cellName] = cell;
                }
                rows.Add(row);
            }

            _controller.DeleteLastRow(rows, cellMap);

            Assert.AreEqual(1, rows.Count, "Має залишитися 1 рядок");
            Assert.IsFalse(cellMap.ContainsKey("A2"), "Клітинка A2 має бути видалена");
            Assert.IsFalse(cellMap.ContainsKey("B2"), "Клітинка B2 має бути видалена");
            Assert.IsTrue(cellMap.ContainsKey("A1"), "Клітинка A1 має залишитися");
        }

        [TestMethod]
        public void AddColumn_AddsNewColumnToAllRows()
        {
            var rows = new ObservableCollection<RowViewModel>();
            var headers = new ObservableCollection<string> { "A", "B" };
            var cellMap = new Dictionary<string, CellViewModel>();
            
            for (int i = 1; i <= 2; i++)
            {
                var row = new RowViewModel { RowNumber = $"{i}" };
                for (int j = 0; j < 2; j++)
                {
                    string cellName = $"{headers[j]}{i}";
                    var cell = new CellViewModel { CellName = cellName, Value = "" };
                    row.Cells.Add(cell);
                    cellMap[cellName] = cell;
                }
                rows.Add(row);
            }

            _controller.AddColumn(rows, headers, cellMap, GetColumnName);

            Assert.AreEqual(3, headers.Count, "Має бути 3 стовпці");
            Assert.AreEqual("C", headers[2], "Третій стовпець має бути 'C'");
            Assert.AreEqual(3, rows[0].Cells.Count, "У першому рядку має бути 3 клітинки");
            Assert.AreEqual(3, rows[1].Cells.Count, "У другому рядку має бути 3 клітинки");
            Assert.IsTrue(cellMap.ContainsKey("C1"));
            Assert.IsTrue(cellMap.ContainsKey("C2"));
        }

        [TestMethod]
        public void DeleteLastColumn_RemovesColumnFromAllRows()
        {
            var rows = new ObservableCollection<RowViewModel>();
            var headers = new ObservableCollection<string> { "A", "B", "C" };
            var cellMap = new Dictionary<string, CellViewModel>();
            
            for (int i = 1; i <= 2; i++)
            {
                var row = new RowViewModel { RowNumber = $"{i}" };
                for (int j = 0; j < 3; j++)
                {
                    string cellName = $"{headers[j]}{i}";
                    var cell = new CellViewModel { CellName = cellName, Value = "" };
                    row.Cells.Add(cell);
                    cellMap[cellName] = cell;
                }
                rows.Add(row);
            }
            _controller.DeleteLastColumn(rows, headers, cellMap);

            Assert.AreEqual(2, headers.Count, "Має залишитися 2 стовпці");
            Assert.AreEqual(2, rows[0].Cells.Count);
            Assert.AreEqual(2, rows[1].Cells.Count);
            Assert.IsFalse(cellMap.ContainsKey("C1"));
            Assert.IsFalse(cellMap.ContainsKey("C2"));
        }

        private static string GetColumnName(int index)
        {
            string columnName = "";
            while (index >= 0)
            {
                columnName = (char)('A' + (index % 26)) + columnName;
                index = (index / 26) - 1;
            }
            return columnName;
        }
    }
}