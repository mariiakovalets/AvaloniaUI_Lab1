using Avalonia.Controls;
using TableManager.App.Views;

namespace TableManager.App
{
    public partial class MainWindow : Window
    {
        private object? _previousView;

        public MainWindow()
        {
            InitializeComponent();
            
            this.Opened += (s, e) => NavigateToMain();
        }

        public void NavigateToMain()
        {
            if (MainContent != null)
            {
                _previousView = null;
                var mainView = new MainView(this);
                if (mainView != null)
                {
                    MainContent.Content = mainView;
                }
            }
        }

        public void NavigateToHelp()
        {
            if (MainContent != null)
            {
                _previousView = MainContent.Content;
                var helpView = new HelpView(this);
                if (helpView != null)
                {
                    MainContent.Content = helpView;
                }
            }
        }

        public void NavigateToTable(string? filePath = null)
        {
            var tableView = new TableView();
            if (tableView != null)
            {
                tableView.Initialize(this, filePath);
                
                if (MainContent != null)
                {
                    _previousView = null;
                    MainContent.Content = tableView;
                }
            }
        }

        public void NavigateBack()
        {
            if (MainContent != null && _previousView != null)
            {
                MainContent.Content = _previousView;
                _previousView = null;
            }
            else
            {
                NavigateToMain();
            }
        }
    }
}