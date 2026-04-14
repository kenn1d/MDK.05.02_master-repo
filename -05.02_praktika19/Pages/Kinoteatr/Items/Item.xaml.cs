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
using WpfApp1.Classes;

namespace WpfApp1.Pages.Kinoteatr.Items
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        KinoteatrContext kinoteatr;
        Main main;
        public Item(KinoteatrContext kinoteatr, Main main)
        {
            InitializeComponent();

            name.Text = kinoteatr.Name;
            countZal.Text = kinoteatr.CountZal.ToString();
            count.Text = kinoteatr.Count.ToString();
            this.kinoteatr = kinoteatr; 
            this.main = main;
        }

        private void editRecord(object sender, RoutedEventArgs e) =>
            MainWindow.init.OpenPage(new Pages.Kinoteatr.Add(this.kinoteatr));

        private void deleteRecord(object sender, RoutedEventArgs e)
        {
            kinoteatr.Delete();
            main.parent.Children.Remove(this);
        }
    }
}
