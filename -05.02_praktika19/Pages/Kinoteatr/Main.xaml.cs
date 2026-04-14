using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Classes;

namespace WpfApp1.Pages.Kinoteatr
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        List<KinoteatrContext> AllKinoteatrs = KinoteatrContext.Select();

        public Main()
        {
            InitializeComponent();

            foreach (KinoteatrContext item in AllKinoteatrs)
            {
                parent.Children.Add(new Items.Item(item, this));
            }
        }

        private void AddRecord(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.Kinoteatr.Add());
        }

        private void kinoSearch(object sender, RoutedEventArgs e)
        {
            List<KinoteatrContext> search = AllKinoteatrs;
            if (kinoName.Text != "") search = search.FindAll(x => x.Name == kinoName.Text);
            if (kinoCountZal.Text != "") search = search.FindAll(x => x.CountZal == int.Parse(kinoCountZal.Text));
            if (kinoCount.Text != "") search = search.FindAll(x => x.Count == int.Parse(kinoCount.Text));

            parent.Children.Clear();
            foreach (KinoteatrContext item in search)
            {
                parent.Children.Add(new Items.Item(item, this));
            }
        }
    }
}
