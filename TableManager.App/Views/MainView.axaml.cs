using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace TableManager.App.Views
{
    public partial class MainView : UserControl
    {
        private readonly MainWindow _mainWindow;
        public ObservableCollection<string> RecentFiles { get; set; }

        public MainView(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            
            RecentFiles = new ObservableCollection<string>();
            LoadRecentFiles();
            RecentFilesList.ItemsSource = RecentFiles;
        }

        private void CreateTable_Click(object? sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToTable();
        }

        private void Help_Click(object? sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToHelp();
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
                _mainWindow.NavigateToTable(filePath);
            }
        }

        private void RecentFile_Selected(object? sender, SelectionChangedEventArgs e)
        {
            if (RecentFilesList.SelectedItem is string filePath)
            {
                if (File.Exists(filePath))
                {
                    _mainWindow.NavigateToTable(filePath);
                }
                else
                {
                    RecentFiles.Remove(filePath);
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
                        RecentFiles.Add(line);
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
            File.WriteAllLines(recentFilePath, RecentFiles);
        }

        private void AddRecentFile(string filePath)
        {
            if (RecentFiles.Contains(filePath))
                RecentFiles.Remove(filePath);
            
            RecentFiles.Insert(0, filePath);
            
            while (RecentFiles.Count > 10)
                RecentFiles.RemoveAt(RecentFiles.Count - 1);
            
            SaveRecentFiles();
        }
    }
}