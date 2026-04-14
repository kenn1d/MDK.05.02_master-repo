using System;
using System.Windows;
using System.Windows.Controls;

namespace praktika18.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.Ticket(from.Text, to.Text, timeStart.Text));
        }
    }
}
