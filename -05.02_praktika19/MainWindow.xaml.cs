using System.Windows;
using System.Windows.Controls;
using WpfApp1.Classes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow init;

        public MainWindow()
        {
            InitializeComponent();
            OpenPage(new Pages.Afisha.Main());
            init = this;
        }

        public void OpenPage(Page Page)
        {
            frame.Navigate(Page);
        }

        private void openKino(object sender, RoutedEventArgs e)
        {
            OpenPage(new Pages.Kinoteatr.Main());
        }

        private void openAfisha(object sender, RoutedEventArgs e)
        {
            OpenPage(new Pages.Afisha.Main());
        }

        private void openBilet(object sender, RoutedEventArgs e)
        {
            OpenPage(new Pages.Bilet.Main());
        }
    }
}
