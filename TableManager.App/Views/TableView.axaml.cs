using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Avalonia.Markup.Xaml;
using Avalonia.Input;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using TableManager.App.Controllers;

namespace TableManager.App.Views
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private string _value = "";
        private string _displayValue = "";
        private bool _hasError = false;
        private string _errorMessage = "";
        private bool _isFocused = false;

        public string Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }

        public string DisplayValue
        {
            get => _displayValue;
            set
            {
                if (_displayValue != value)
                {
                    _displayValue = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DisplayText));
                }
            }
        }

        public string CellName { get; set; } = "";

        public bool HasError
        {
            get => _hasError;
            set
            {
                if (_hasError != value)
                {
                    _hasError = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsFocused
        {
            get => _isFocused;
            set
            {
                if (_isFocused != value)
                {
                    _isFocused = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DisplayText));
                }
            }
        }
        public string DisplayText
        {
            get => IsFocused ? Value : (string.IsNullOrEmpty(DisplayValue) ? Value : DisplayValue);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RowViewModel
    {
        public string RowNumber { get; set; } = "";
        public ObservableCollection<CellViewModel> Cells { get; set; } = new();
    }
    public partial class TableView : UserControl
    {
        private MainWindow? _mainWindow;

        private string? _currentFilePath;

        public ObservableCollection<RowViewModel> TableRows { get; set; }
        public ObservableCollection<string> ColumnHeaders { get; set; }

        private Dictionary<string, CellViewModel> _cellMap = new();

        private HashSet<string> _changedCells = new();

        private readonly UIController _uiController;
        private readonly FileManager _fileManager;

        public TableView()
        {
            AvaloniaXamlLoader.Load(this);

            TableRows = new ObservableCollection<RowViewModel>();
            ColumnHeaders = new ObservableCollection<string>();

            _uiController = new UIController();
            _fileManager = new FileManager();

            DataContext = this;
        }

        public void Initialize(MainWindow mainWindow, string? filePath = null)
        {
            _mainWindow = mainWindow;

            var createPanel = this.FindControl<StackPanel>("CreateTablePanel");
            var editPanel = this.FindControl<StackPanel>("EditTablePanel");
            var modePanel = this.FindControl<StackPanel>("ModePanel");

            if (!string.IsNullOrEmpty(filePath))
            {
                _currentFilePath = filePath;

                var loaded = _fileManager.LoadTableFromFile(filePath, GetColumnName);

                ColumnHeaders.Clear();
                foreach (var h in loaded.headers)
                    ColumnHeaders.Add(h);

                TableRows.Clear();
                foreach (var r in loaded.rows)
                    TableRows.Add(r);

                _cellMap = loaded.map;

                _uiController.RecalculateAll(TableRows, _cellMap);

                if (createPanel != null) createPanel.IsVisible = false;
                if (editPanel != null) editPanel.IsVisible = true;
                if (modePanel != null) modePanel.IsVisible = true;
            }
            else
            {
                if (createPanel != null) createPanel.IsVisible = true;
                if (editPanel != null) editPanel.IsVisible = false;
                if (modePanel != null) modePanel.IsVisible = false;
            }
        }

        private void Home_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
            => _mainWindow?.NavigateToMain();

        private void Help_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
            => _mainWindow?.NavigateToHelp();

        private void CreateTable_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var rowsBox = this.FindControl<TextBox>("RowsBox");
            var colsBox = this.FindControl<TextBox>("ColsBox");

            if (rowsBox == null || colsBox == null) return;

            if (int.TryParse(rowsBox.Text, out int rows) &&
                int.TryParse(colsBox.Text, out int cols))
            {
                TableRows.Clear();
                ColumnHeaders.Clear();
                _cellMap.Clear();
                _changedCells.Clear();

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
                var createPanel = this.FindControl<StackPanel>("CreateTablePanel");
                var editPanel = this.FindControl<StackPanel>("EditTablePanel");
                var modePanel = this.FindControl<StackPanel>("ModePanel");

                if (createPanel != null) createPanel.IsVisible = false;
                if (editPanel != null) editPanel.IsVisible = true;
                if (modePanel != null) modePanel.IsVisible = true;
            }
        }
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
                _fileManager.SaveTableToFile(_currentFilePath, TableRows);
            }
        }
        private void DeleteTable_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            string? deletedFilePath = _currentFilePath;
            _fileManager.DeleteFileIfExists(_currentFilePath);

            TableRows.Clear();
            ColumnHeaders.Clear();
            _cellMap.Clear();
            _changedCells.Clear(); 


            if (!string.IsNullOrEmpty(deletedFilePath))
            {
                _fileManager.RemoveFromRecentFiles(deletedFilePath);
            }

            _mainWindow?.NavigateToMain();
        }

        
        private void Cell_GotFocus(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.Tag is CellViewModel cell)
            {
                cell.IsFocused = true;
            }
        }

        private void Cell_LostFocus(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.Tag is CellViewModel cell)
            {
                string newValue = textBox.Text ?? "";
                
                if (cell.Value != newValue)
                {
                    cell.Value = newValue;
                    cell.DisplayValue = ""; 
                    _changedCells.Add(cell.CellName);
                }
                
                cell.IsFocused = false;
            }
        }
        private void Calculate_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _uiController.RecalculateChanged(_changedCells, _cellMap);

            foreach (var row in TableRows)
                foreach (var cell in row.Cells)
                    cell.OnPropertyChanged(nameof(cell.DisplayText));
        }
        private void AddRow_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _uiController.AddRow(TableRows, ColumnHeaders, _cellMap, GetColumnName);
        }

        private void DeleteRow_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _uiController.DeleteLastRow(TableRows, _cellMap);
        }

        private void AddColumn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _uiController.AddColumn(TableRows, ColumnHeaders, _cellMap, GetColumnName);
        }

        private void DeleteColumn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _uiController.DeleteLastColumn(TableRows, ColumnHeaders, _cellMap);
        }

       /*  private void Clear_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _uiController.ClearAll(TableRows);
        } */

        private void ShowExpressions_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _uiController.ForceShowExpressions(TableRows);

            var expressionsBtn = this.FindControl<Button>("ShowExpressionsButton");
            var valuesBtn = this.FindControl<Button>("ShowValuesButton");

            if (expressionsBtn != null)
            {
                expressionsBtn.Classes.Remove("mode-inactive");
                expressionsBtn.Classes.Add("mode-active");
            }

            if (valuesBtn != null)
            {
                valuesBtn.Classes.Remove("mode-active");
                valuesBtn.Classes.Add("mode-inactive");
            }
        }
        private void ShowValues_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _uiController.ForceShowValues(TableRows);

            var expressionsBtn = this.FindControl<Button>("ShowExpressionsButton");
            var valuesBtn = this.FindControl<Button>("ShowValuesButton");

            if (valuesBtn != null)
            {
                valuesBtn.Classes.Remove("mode-inactive");
                valuesBtn.Classes.Add("mode-active");
            }

            if (expressionsBtn != null)
            {
                expressionsBtn.Classes.Remove("mode-active");
                expressionsBtn.Classes.Add("mode-inactive");
            }
        }
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
    }
}
