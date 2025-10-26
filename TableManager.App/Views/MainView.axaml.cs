using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace TableManager.App.Views
{
    public class RecentFileItem
    {
        public string FileName { get; set; } = "";
        public string FullPath { get; set; } = "";
    }

    public partial class MainView : UserControl
    {
        private MainWindow? _mainWindow;
        public ObservableCollection<RecentFileItem> RecentFiles { get; set; } 

        public MainView()
        {
            InitializeComponent();
            RecentFiles = new ObservableCollection<RecentFileItem>(); 
        }

        public MainView(MainWindow mainWindow) : this()
        {
            _mainWindow = mainWindow;
            LoadRecentFiles();
            
            var recentFilesList = this.FindControl<ListBox>("RecentFilesList");
            if (recentFilesList != null)
            {
                recentFilesList.ItemsSource = RecentFiles;
            }
        }

        private void CreateTable_Click(object? sender, RoutedEventArgs e)
        {
            _mainWindow?.NavigateToTable();
        }

        private void Help_Click(object? sender, RoutedEventArgs e)
        {
            _mainWindow?.NavigateToHelp();
        }

        private async void OpenFile_Click(object? sender, RoutedEventArgs e)
        {
            var topLevel = TopLevel.GetTopLevel(this);
            if (topLevel == null) return;

            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Відкрити таблицю",
                AllowMultiple = false,
                FileTypeFilter = new[]
                {
                    new FilePickerFileType("Table files") { Patterns = new[] { "*.table" } }
                }
            });

            if (files.Count > 0)
            {
                var filePath = files[0].Path.LocalPath;
                AddRecentFile(filePath);
                _mainWindow?.NavigateToTable(filePath);
            }
        }

        private void RecentFile_Selected(object? sender, SelectionChangedEventArgs e)
        {
            var recentFilesList = this.FindControl<ListBox>("RecentFilesList");
            if (recentFilesList?.SelectedItem is RecentFileItem fileItem) 
            {
                if (File.Exists(fileItem.FullPath))
                {
                    _mainWindow?.NavigateToTable(fileItem.FullPath); 
                }
                else
                {
                    RecentFiles.Remove(fileItem);
                    SaveRecentFiles();
                }
            }
        }

        private void ClearRecent_Click(object? sender, RoutedEventArgs e)
        {
            RecentFiles.Clear();
            SaveRecentFiles();
        }

        private void LoadRecentFiles()
        {
            var recentFilePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "TableManager",
                "recent.txt"
            );

            if (File.Exists(recentFilePath))
            {
                var lines = File.ReadAllLines(recentFilePath);
                foreach (var line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        RecentFiles.Add(new RecentFileItem
                        {
                            FileName = Path.GetFileName(line),
                            FullPath = line
                        });
                    }
                }
            }
        }

        private void SaveRecentFiles()
        {
            var appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "TableManager"
            );
            
            Directory.CreateDirectory(appDataPath);
            
            var recentFilePath = Path.Combine(appDataPath, "recent.txt");
            File.WriteAllLines(recentFilePath, RecentFiles.Select(f => f.FullPath));
        }

        private void AddRecentFile(string filePath)
        {
            var existing = RecentFiles.FirstOrDefault(f => f.FullPath == filePath);
            if (existing != null)
                RecentFiles.Remove(existing);
            
            RecentFiles.Insert(0, new RecentFileItem
            {
                FileName = Path.GetFileName(filePath),
                FullPath = filePath
            });
            
            while (RecentFiles.Count > 10)
                RecentFiles.RemoveAt(RecentFiles.Count - 1);
            
            SaveRecentFiles();
        }
    }
}