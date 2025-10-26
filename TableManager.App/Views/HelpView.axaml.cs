using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TableManager.App.Views
{
    public partial class HelpView : UserControl
    {
        private MainWindow? _mainWindow; 

        public HelpView()
        {
            InitializeComponent();
        }

        public HelpView(MainWindow mainWindow) : this() 
        {
            _mainWindow = mainWindow;
        }

        private void BackToMain_Click(object? sender, RoutedEventArgs e)
        {
            _mainWindow?.NavigateToMain();
        }
        
        private void Back_Click(object? sender, RoutedEventArgs e)
        {
            _mainWindow?.NavigateBack();
        }
    }
}