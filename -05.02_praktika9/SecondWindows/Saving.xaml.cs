using System;
using System.Collections.Generic;
using System.IO;
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

namespace praktika9.SecondWindows
{
    /// <summary>
    /// Логика взаимодействия для Saving.xaml
    /// </summary>
    public partial class Saving : Window
    {
        List<Classes.Book> AllBooks = Classes.Book.AllBooks;

        public Saving()
        {
            InitializeComponent();
        }

        private void save(object sender, RoutedEventArgs e)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path.Text, false))
                {

                    foreach (Classes.Book book in AllBooks)
                    {
                        sw.WriteLine($"{book.Id} | {book.Name} | {book.ToGenres()} | {book.ToAuthors()} | {book.Year}");
                    }
                    sw.Close();
                }

                MessageBox.Show("Репозиторий успешно сохранён :)", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch {
                MessageBox.Show("Проверьте правильность ввода директории.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }
    }
}
