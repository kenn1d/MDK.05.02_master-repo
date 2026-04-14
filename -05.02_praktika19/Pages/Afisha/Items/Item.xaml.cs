using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Classes;
using WpfApp1.Model;

namespace WpfApp1.Pages.Afisha.Items
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        List<KinoteatrContext> AllKinoteatrs = KinoteatrContext.Select();
        List<BiletContext> AllBilets = BiletContext.Select();
        AfishaContext item;
        Main main;

        public Item(AfishaContext item, Main main)
        {
            InitializeComponent();

            kinoteatrs.Text = AllKinoteatrs.Find(x => x.Id == item.IdKinoteatr).Name;
            name.Text = item.Name;
            date.Text = item.Time.ToString("yyyy-MM-dd");
            time.Text = item.Time.ToString("HH:mm");
            price.Text = item.Price.ToString();
            this.item = item;
            this.main = main;
        }

        private void editRecord(object sender, System.Windows.RoutedEventArgs e) =>
            MainWindow.init.OpenPage(new Pages.Afisha.Add(this.item));

        private void deleteRecord(object sender, System.Windows.RoutedEventArgs e)
        {
            item.Delete();
            main.parent.Children.Remove(this);
        }

        private void byRecord(object sender, System.Windows.RoutedEventArgs e)
        {
            BiletContext newBilet = new BiletContext(
                0,
                item.Id
                );
            newBilet.Add();
            MessageBox.Show("Запись добавлена.");
        }
    }
}
