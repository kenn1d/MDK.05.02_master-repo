using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace praktika14.Elements
{
    /// <summary>
    /// Логика взаимодействия для Category.xaml
    /// </summary>
    public partial class Category : UserControl
    {
        List<Classes.Item> categoryItems;

        public Category(Classes.Category category)
        {
            InitializeComponent();

            namecat.Content = category.name;
            this.categoryItems = category.items;
        }

        private void NextPage(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = (MainWindow)Application.Current.MainWindow;
            mainwindow.OpenPage(MainWindow.pages.Main, this.categoryItems);
        }
    }
}
