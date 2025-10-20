
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TableManager.App.Controllers;

namespace TableManager.App.Views
{
    public class CellViewModel
    {
        public string Value { get; set; } = "";
        public string DisplayValue { get; set; } = "";
        public string CellName { get; set; } = "";
        public bool HasError { get; set; } = false;
        public string ErrorMessage { get; set; } = "";
    }

    public class RowViewModel
    {
        public string RowNumber { get; set; } = "";
        public ObservableCollection<CellViewModel> Cells { get; set; } = new();
    }

    public partial class TableView : UserControl
    {
        private readonly MainWindow _mainWindow;
        private string? _currentFilePath;
        public ObservableCollection<RowViewModel> TableRows { get; set; }
        public ObservableCollection<string> ColumnHeaders { get; set; }
        private Dictionary<string, CellViewModel> _cellMap = new();

        public TableView(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            TableRows = new ObservableCollection<RowViewModel>();
            ColumnHeaders = new ObservableCollection<string>();
            DataContext = this;
        }

        public TableView(MainWindow mainWindow, string filePath) : this(mainWindow)
        {
            _currentFilePath = filePath;
            LoadTableFromFile(filePath);
        }

        private void Home_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
            => _mainWindow.NavigateToMain();

        private void Help_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
            => _mainWindow.NavigateToHelp();

        private async void Save_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var topLevel = TopLevel.GetTopLevel(this);
            if (topLevel == null) return;

            var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
            {
                Title = "Зберегти таблицю",
                SuggestedFileName = "table1.table",
                FileTypeChoices = new[]
                {
                    new FilePickerFileType("Table files") { Patterns = new[] { "*.table" } }
                }
            });

            if (file != null)
            {
                _currentFilePath = file.Path.LocalPath;
                await using var stream = await file.OpenWriteAsync();
                using var writer = new StreamWriter(stream);
                
                foreach (var row in TableRows)
                {
                    var values = new List<string>();
                    foreach (var cell in row.Cells)
                    {
                        values.Add(cell.Value);
                    }
                    writer.WriteLine(string.Join(",", values));
                }
            }
        }

        private void CreateTable_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (int.TryParse(RowsBox.Text, out int rows) && int.TryParse(ColsBox.Text, out int cols))
            {
                TableRows.Clear();
                ColumnHeaders.Clear();
                _cellMap.Clear();
                
                
                for (int j = 0; j < cols; j++)
                {
                    ColumnHeaders.Add(GetColumnName(j));
                }
                
       
                for (int i = 0; i < rows; i++)
                {
                    var rowVM = new RowViewModel { RowNumber = $"{i + 1}" };
                    
                    for (int j = 0; j < cols; j++)
                    {
                        string cellName = $"{GetColumnName(j)}{i + 1}";
                        var cellVM = new CellViewModel 
                        { 
                            Value = "",
                            DisplayValue = "",
                            CellName = cellName
                        };
                        
                        rowVM.Cells.Add(cellVM);
                        _cellMap[cellName] = cellVM;
                    }
                    
                    TableRows.Add(rowVM);
                }

              
                SubscribeToCellChanges();
            }
        }

        private void SubscribeToCellChanges()
        {
            foreach (var row in TableRows)
            {
                foreach (var cell in row.Cells)
                {
                    
                }
            }
        }

        public void CalculateCell(CellViewModel cell)
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
            foreach (var kvp in _cellMap)
            {
                allCells[kvp.Key] = kvp.Value.Value;
            }

            if (Evaluator.DetectCycle(cell.CellName, allCells))
            {
                cell.DisplayValue = "#CYCLE";
                cell.HasError = true;
                cell.ErrorMessage = "Циклічне посилання";
                return;
            }

      
            var cellValues = new Dictionary<string, string>();
            foreach (var kvp in _cellMap)
            {
                cellValues[kvp.Key] = kvp.Value.Value;
            }

            var result = Evaluator.Evaluate(cell.Value, cellValues);
            
            if (result.success)
            {
                // Для варіанту 24 - всі значення інтерпретуємо як логічні
                bool logicalResult = result.result != 0;
                cell.DisplayValue = logicalResult ? "TRUE" : "FALSE";
                cell.HasError = false;
                cell.ErrorMessage = "";
            }
            else
            {
                cell.DisplayValue = result.error.StartsWith("#") ? result.error : "#ERROR";
                cell.HasError = true;
                cell.ErrorMessage = result.error;
            }
        }

        // Перерахунок всіх клітинок
        private void RecalculateAll()
        {
            foreach (var row in TableRows)
            {
                foreach (var cell in row.Cells)
                {
                    CalculateCell(cell);
                }
            }
        }

        // Метод для отримання назви стовпця (A, B, C, ..., Z, AA, AB, ...)
        private string GetColumnName(int index)
        {
            string columnName = "";
            while (index >= 0)
            {
                columnName = (char)('A' + (index % 26)) + columnName;
                index = (index / 26) - 1;
            }
            return columnName;
        }

private void Cell_LostFocus(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
{
    if (sender is TextBox textBox && textBox.Tag is CellViewModel cell)
    {
        CalculateCell(cell);
        RecalculateAll(); 
    }
}
        private void LoadTableFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                TableRows.Clear();
                ColumnHeaders.Clear();
                _cellMap.Clear();
                
                var lines = File.ReadAllLines(filePath);
                
                if (lines.Length > 0)
                {
                    var firstLine = lines[0].Split(',');
                    for (int j = 0; j < firstLine.Length; j++)
                    {
                        ColumnHeaders.Add(GetColumnName(j));
                    }
                }
                
                for (int i = 0; i < lines.Length; i++)
                {
                    var values = lines[i].Split(',');
                    var rowVM = new RowViewModel { RowNumber = $"{i + 1}" };
                    
                    for (int j = 0; j < values.Length; j++)
                    {
                        string cellName = $"{GetColumnName(j)}{i + 1}";
                        var cellVM = new CellViewModel 
                        { 
                            Value = values[j],
                            CellName = cellName
                        };
                        
                        rowVM.Cells.Add(cellVM);
                        _cellMap[cellName] = cellVM;
                    }
                    
                    TableRows.Add(rowVM);
                }

                RecalculateAll();
            }
        }
    }
}