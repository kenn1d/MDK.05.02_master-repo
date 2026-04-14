using praktika15.Classes;
using praktika15.Models;
using System.Collections.Generic;
using System.Windows;
namespace praktika15
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow init;
        public List<DocumentContext> AllDocuments = new DocumentContext().AllDocuments();
        public List<UserContext> AllUsers = new UserContext().AllUsers();
        public MainWindow()
        {
            InitializeComponent();

            init = this;

            frame.Navigate(new Pages.Main());
        }
    }
}
