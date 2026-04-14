using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Classes;
using WpfApp1.Model;

namespace WpfApp1.Pages.Afisha
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        List<AfishaContext> AllAfishas = AfishaContext.Select();
        List<KinoteatrContext> AllKinoteatrs = KinoteatrContext.Select();
        public Main()
        {
            InitializeComponent();

            afishaKino.Items.Add("Выберите ...");

            foreach (var item in AllKinoteatrs)
            {
                afishaKino.Items.Add(item.Name);
            }

            foreach (AfishaContext item in AllAfishas)
            {
                parent.Children.Add(new Items.Item(item, this));
            }
        }

        private void AddRecord(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.Afisha.Add());
        }

        private void afishaSearch(object sender, RoutedEventArgs e)
        {
            List<AfishaContext> search = AllAfishas;
            if (afishaName.Text != "") search = search.FindAll(x => x.Name == afishaName.Text);
            if (afishaKino.SelectedIndex > 0) search = search.FindAll(x => x.IdKinoteatr == AllKinoteatrs.Find(y => y.Name == afishaKino.Text).Id);
            if (afishaPrice.Text != "") search = search.FindAll(x => x.Price == int.Parse(afishaPrice.Text));
            if (afishaDate.Text != "") search = search.FindAll(x => x.Time.ToString("dd.MM.yyyy") == afishaDate.Text);

            parent.Children.Clear();
            foreach (AfishaContext item in search)
            {
                parent.Children.Add(new Items.Item(item, this));
            }
        }
    }
}
