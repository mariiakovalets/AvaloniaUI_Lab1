using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using TableManager.App.Views; 

namespace TableManager.App.Controllers
{
    public class FileManager
    {
        public void SaveTableToFile(string path, ObservableCollection<RowViewModel> rows)
        {
            using var writer = new StreamWriter(path);

            foreach (var row in rows)
            {
                var values = row.Cells.Select(c => c.Value ?? "");
                writer.WriteLine(string.Join(",", values));
            }
        }

        public (List<string> headers,
                ObservableCollection<RowViewModel> rows,
                Dictionary<string, CellViewModel> map)
            LoadTableFromFile(string path, Func<int, string> getColumnName)
        {
            var tableRows = new ObservableCollection<RowViewModel>();
            var columnHeaders = new List<string>();
            var cellMap = new Dictionary<string, CellViewModel>();

            if (!File.Exists(path))
                return (columnHeaders, tableRows, cellMap);

            var lines = File.ReadAllLines(path);

            if (lines.Length > 0)
            {
                var firstLine = lines[0].Split(',');
                for (int j = 0; j < firstLine.Length; j++)
                {
                    columnHeaders.Add(getColumnName(j));
                }
            }

            for (int i = 0; i < lines.Length; i++)
            {
                var values = lines[i].Split(',');
                var rowVM = new RowViewModel { RowNumber = $"{i + 1}" };

                for (int j = 0; j < values.Length; j++)
                {
                    string cellName = $"{getColumnName(j)}{i + 1}";
                    var cellVM = new CellViewModel
                    {
                        Value = values[j],
                        DisplayValue = "",
                        CellName = cellName
                    };

                    rowVM.Cells.Add(cellVM);
                    cellMap[cellName] = cellVM;
                }

                tableRows.Add(rowVM);
            }

            return (columnHeaders, tableRows, cellMap);
        }

        public void DeleteFileIfExists(string? path)
        {
            if (string.IsNullOrEmpty(path))
                return;

            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Помилка видалення файлу: {ex.Message}");
            }
        }

        public void RemoveFromRecentFiles(string filePath)
        {
            var recentFilePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "TableManager",
                "recent.txt"
            );

            try
            {
                if (File.Exists(recentFilePath))
                {
                    var lines = File.ReadAllLines(recentFilePath).ToList();
                    lines.Remove(filePath);
                    File.WriteAllLines(recentFilePath, lines);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Помилка видалення з recent: {ex.Message}");
            }
        }
    }
}
