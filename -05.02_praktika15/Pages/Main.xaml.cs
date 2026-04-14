using praktika15.Classes;
using System.Windows;
using System.Windows.Controls;

namespace praktika15.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            CreateUI();
        }

        public void CreateUI()
        {
            Parent.Children.Clear();
            foreach (DocumentContext item in MainWindow.init.AllDocuments)
                Parent.Children.Add(new Elements.Item(item));
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.Close();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.frame.Navigate(new Pages.Add());
        }

        private void Go(object sender, RoutedEventArgs e)
        {
            MainWindow.init.frame.Navigate(new Pages.ViewUsers());
        }
    }
}
