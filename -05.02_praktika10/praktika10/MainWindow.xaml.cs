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

namespace praktika10
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Classes.Passport> Passports = new List<Classes.Passport>();
        public List<Classes.Passport> FindedPassports = new List<Classes.Passport>();
        public static MainWindow init;

        public MainWindow()
        {
            InitializeComponent();
            init = this;
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            new Windows.Add(null).ShowDialog();
        }
        private void Update(object sender, RoutedEventArgs e)
        {
            // Если выбран какой-то паспорт
            if (lv_passport.SelectedIndex > -1)
                new Windows.Add(lv_passport.SelectedItem as Classes.Passport).ShowDialog();
            else
                MessageBox.Show("Выберите элемент для изменения");
        }
        private void Delete(object sender, RoutedEventArgs e) { 
            // Если выбран какой-то паспорт
            if (lv_passport.SelectedIndex > -1)
            {
                Passports.Remove(lv_passport.SelectedItem as Classes.Passport);
                LoadPassports();
            }
        }

        public void LoadPassports()
        {
            // Чистим список
            lv_passport.Items.Clear();
            // Добавляем паспорт в список
            foreach(Classes.Passport Passport in Passports)
                lv_passport.Items.Add(Passport);
        }

        public void LoadFindedPassports()
        {
            lv_passport.Items.Clear();
            foreach (Classes.Passport FindedPassports in FindedPassports)
                lv_passport.Items.Add(FindedPassports);
            FindedPassports.Clear();
        }

        private void Find(object sender, RoutedEventArgs e)
        {
            new Windows.FindFIO(Passports).ShowDialog();
        }

        private void Return(object sender, RoutedEventArgs e)
        {
            LoadPassports();
        }
    }
}
