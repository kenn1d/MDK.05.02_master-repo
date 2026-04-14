using System.Windows;

namespace praktika27
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow init;
        public View.Main Main = new View.Main();
        public View.MainCategory MainCategory = new View.MainCategory();

        public MainWindow()
        {
            init = this;
            InitializeComponent();
            frame.Navigate(Main);
        }

        private void OpenIndexItems(object sender, RoutedEventArgs e) =>
            frame.Navigate(Main);

        private void OpenIndexCategorys(object sender, RoutedEventArgs e) =>
            frame.Navigate(MainCategory);
    }
}