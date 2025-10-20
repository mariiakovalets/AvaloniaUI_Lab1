using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TableManager.App.Views
{
    public partial class HelpView : UserControl
    {
        private readonly MainWindow _mainWindow;

        public HelpView(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void BackToMain_Click(object? sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToMain();
        }
    }
}