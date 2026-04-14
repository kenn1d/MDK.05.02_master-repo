using System.Windows;

namespace praktika13
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string localPath;

        public MainWindow()
        {
            InitializeComponent();
            localPath = System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName).FullName; // путь к папке с программой

            OpenPages(pages.main);
        }

        public enum pages
        {
            main
        }

        public void OpenPages(pages _pages)
        {
            if (_pages == pages.main) frame.Navigate(new Layouts.Main(this));
        }
    }
}
