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
using System.Windows.Shapes;

namespace praktika10.Windows
{
    /// <summary>
    /// Логика взаимодействия для FindFIO.xaml
    /// </summary>
    public partial class FindFIO : Window
    {
        List<Classes.Passport> AllPassports = new List<Classes.Passport>();
        public FindFIO(List<Classes.Passport> Passports)
        {
            InitializeComponent();
            AllPassports = Passports;
        }

        private void Find(object sender, RoutedEventArgs e)
        {
            // TODO: Поиск только по полному ФИО
            //if (f_Name.Text != "" || s_Name.Text != "" || l_Name.Text != "") {
            //    Classes.Passport people = AllPassports.Find(x => x.Name == s_Name.Text && x.FirstName == f_Name.Text && x.LastName == l_Name.Text);
            //    if (people != null) { MainWindow.init.FindedPassports.Add(people); MainWindow.init.LoadFindedPassports(); Close(); }
            //    else { MessageBox.Show("Такой человек не найден.", "Внимаение!", MessageBoxButton.OK, MessageBoxImage.Hand); return; };
            //}
            //else { MessageBox.Show("Пожалуйста, заполните все поля.", "Внимаение!", MessageBoxButton.OK, MessageBoxImage.Hand); return; };

            // TODO: Поиск только по элементам ФИО
            List<Classes.Passport> findPassports = new List<Classes.Passport>();
            if (f_Name.Text != "")
            {
                findPassports = AllPassports.FindAll(x => x.FirstName == f_Name.Text);
            }
            if (s_Name.Text != "")
            {
                if (findPassports.Count != 0) findPassports = findPassports.FindAll(x => x.Name == s_Name.Text);
                else findPassports = AllPassports.FindAll(x => x.Name == s_Name.Text);
            }
            if (l_Name.Text != "")
            {
                if (findPassports.Count != 0) findPassports = findPassports.FindAll(x => x.Name == l_Name.Text);
                else findPassports = AllPassports.FindAll(x => x.Name == l_Name.Text);
            }
            if (findPassports.Count == 0) { MessageBox.Show("Такая информация не найдена.", "Внимаение!", MessageBoxButton.OK, MessageBoxImage.Hand); return; }
            else
            {
                foreach (Classes.Passport passport in findPassports)
                {
                    MainWindow.init.FindedPassports.Add(passport);
                }
                MainWindow.init.LoadFindedPassports();
                Close();
            }
        }
    }
}
