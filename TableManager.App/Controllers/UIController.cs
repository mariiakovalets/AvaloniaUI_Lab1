using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TableManager.App.Views; 
using TableManager.App.Controllers;

namespace TableManager.App.Controllers
{
    public class UIController
    {
        public void CalculateCell(
            CellViewModel cell,
            Dictionary<string, CellViewModel> cellMap)
        {
            if (string.IsNullOrWhiteSpace(cell.Value))
            {
                cell.DisplayValue = "";
                cell.HasError = false;
                cell.ErrorMessage = "";
                return;
            }

            if (!cell.Value.StartsWith("="))
            {
                cell.DisplayValue = cell.Value;
                cell.HasError = false;
                cell.ErrorMessage = "";
                return;
            }
            var syntaxCheck = Parser.CheckSyntax(cell.Value);
            if (!syntaxCheck.isValid)
            {
                cell.DisplayValue = "#ERROR";
                cell.HasError = true;
                cell.ErrorMessage = syntaxCheck.error;
                return;
            }

            var allCells = new Dictionary<string, string>();
            foreach (var kvp in cellMap)
                allCells[kvp.Key] = kvp.Value.Value;

            if (Evaluator.DetectCycle(cell.CellName, allCells))
            {
                cell.DisplayValue = "#CYCLE";
                cell.HasError = true;
                cell.ErrorMessage = "Циклічне посилання";
                return;
            }

            var cellValues = new Dictionary<string, string>();
            foreach (var kvp in cellMap)
                cellValues[kvp.Key] = kvp.Value.Value;

            var result = Evaluator.Evaluate(cell.Value, cellValues);

            if (result.Success)
            {
                cell.DisplayValue = result.GetDisplayValue();
                cell.HasError = false;
                cell.ErrorMessage = "";
            }
            else
            {
                cell.DisplayValue = result.Error.StartsWith("#") ? result.Error : "#ERROR";
                cell.HasError = true;
                cell.ErrorMessage = result.Error;
            }
        }

        public void RecalculateAll(
            ObservableCollection<RowViewModel> rows,
            Dictionary<string, CellViewModel> cellMap)
        {
            foreach (var row in rows)
                foreach (var cell in row.Cells)
                    CalculateCell(cell, cellMap);
        }

        public void RecalculateChanged(
            HashSet<string> changedCells,
            Dictionary<string, CellViewModel> cellMap)
        {
            if (changedCells.Count == 0)
                return;

            var allToRecalc = new HashSet<string>(changedCells);
            var dependents = FindAllDependents(changedCells, cellMap);

            foreach (var dep in dependents)
                allToRecalc.Add(dep);

            foreach (var cellName in allToRecalc)
            {
                if (cellMap.ContainsKey(cellName))
                {
                    CalculateCell(cellMap[cellName], cellMap);
                }
            }

            changedCells.Clear();
        }
        public HashSet<string> FindAllDependents(
            HashSet<string> changedCells,
            Dictionary<string, CellViewModel> cellMap)
        {
            var result = new HashSet<string>();
            var queue = new Queue<string>(changedCells);
            var visited = new HashSet<string>();

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (visited.Contains(current)) continue;
                visited.Add(current);

                foreach (var kvp in cellMap)
                {
                    var candidate = kvp.Value;
                    if (candidate.Value.StartsWith("="))
                    {
                        var deps = Parser.GetDependencies(candidate.Value);
                        if (deps.Contains(current))
                        {
                            if (!result.Contains(kvp.Key))
                            {
                                result.Add(kvp.Key);
                                queue.Enqueue(kvp.Key);
                            }
                        }
                    }
                }
            }

            return result;
        }
        public void AddRow(
            ObservableCollection<RowViewModel> rows,
            ObservableCollection<string> headers,
            Dictionary<string, CellViewModel> cellMap,
            Func<int, string> getColumnName)
        {
            if (rows.Count == 0) return;

            int newRowIndex = rows.Count + 1;
            int colCount = rows[0].Cells.Count;

            var newRow = new RowViewModel { RowNumber = $"{newRowIndex}" };

            for (int j = 0; j < colCount; j++)
            {
                string colName = getColumnName(j);
                string cellName = $"{colName}{newRowIndex}";

                var cellVM = new CellViewModel
                {
                    Value = "",
                    DisplayValue = "",
                    CellName = cellName
                };

                newRow.Cells.Add(cellVM);
                cellMap[cellName] = cellVM;
            }

            rows.Add(newRow);
        }
        public void DeleteLastRow(
            ObservableCollection<RowViewModel> rows,
            Dictionary<string, CellViewModel> cellMap)
        {
            if (rows.Count == 0) return;

            var lastRow = rows[rows.Count - 1];
            foreach (var c in lastRow.Cells)
            {
                cellMap.Remove(c.CellName);
            }

            rows.RemoveAt(rows.Count - 1);
        }

        public void AddColumn(
            ObservableCollection<RowViewModel> rows,
            ObservableCollection<string> headers,
            Dictionary<string, CellViewModel> cellMap,
            Func<int, string> getColumnName)
        {
            if (rows.Count == 0) return;

            int newColIndex = headers.Count;
            string newColName = getColumnName(newColIndex);

            headers.Add(newColName);

            for (int i = 0; i < rows.Count; i++)
            {
                string cellName = $"{newColName}{i + 1}";
                var cellVM = new CellViewModel
                {
                    Value = "",
                    DisplayValue = "",
                    CellName = cellName
                };

                rows[i].Cells.Add(cellVM);
                cellMap[cellName] = cellVM;
            }
        }
        public void DeleteLastColumn(
            ObservableCollection<RowViewModel> rows,
            ObservableCollection<string> headers,
            Dictionary<string, CellViewModel> cellMap)
        {
            if (headers.Count == 0) return;

            for (int i = 0; i < rows.Count; i++)
            {
                var row = rows[i];
                var lastCell = row.Cells[row.Cells.Count - 1];
                cellMap.Remove(lastCell.CellName);
                row.Cells.RemoveAt(row.Cells.Count - 1);
            }

            headers.RemoveAt(headers.Count - 1);
        }



        public void ForceShowExpressions(ObservableCollection<RowViewModel> rows)
        {
            foreach (var row in rows)
            {
                foreach (var cell in row.Cells)
                {
                    cell.IsFocused = true;
                    cell.OnPropertyChanged(nameof(cell.DisplayText));
                }
            }
        }

        public void ForceShowValues(ObservableCollection<RowViewModel> rows)
        {
            foreach (var row in rows)
            {
                foreach (var cell in row.Cells)
                {
                    cell.IsFocused = false;
                    cell.OnPropertyChanged(nameof(cell.DisplayText));
                }
            }
        }
    }
}
