using Avalonia.Controls;
using TableManager.App.Views;

namespace TableManager.App
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            // ВАЖЛИВО: Спочатку InitializeComponent, потім навігація
            this.Opened += (s, e) => NavigateToMain();
        }

        public void NavigateToMain()
        {
            if (MainContent != null)
                MainContent.Content = new MainView(this);
        }

        public void NavigateToHelp()
        {
            if (MainContent != null)
                MainContent.Content = new HelpView(this);
        }

        public void NavigateToTable()
        {
            if (MainContent != null)
                MainContent.Content = new TableView(this);
        }

        public void NavigateToTable(string filePath)
        {
            if (MainContent != null)
                MainContent.Content = new TableView(this, filePath);
        }
    }
}