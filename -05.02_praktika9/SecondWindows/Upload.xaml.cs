using praktika9.Classes;
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
    /// Логика взаимодействия для Upload.xaml
    /// </summary>
    public partial class Upload : Window
    {

        public Upload()
        {
            InitializeComponent();
        }

        private void upload(object sender, RoutedEventArgs e)
        {

            try
            {
                using (StreamReader sr = new StreamReader(path.Text))
                {
                    int StartLine = 0;
                    int NumberLine = 5;
                    while (!sr.EndOfStream)
                    {
                        if (StartLine < NumberLine) { StartLine++; sr.ReadLine(); continue; }
                        string[] book = sr.ReadLine().Split('|');
                        string[] book_genre = book[1].Split(' ');

                        foreach (string y in book_genre) {
                            List<Classes.Genre> genre = Classes.Genre.AllGenres.FindAll(x => x.Name.Contains(y));
                            if (genre.Count == 0)
                            {
                                Classes.Genre newGenre = new Classes.Genre(Classes.Genre.AllGenres.Last().Id + 1, y);
                                Classes.Genre.AllGenres.Add(newGenre);
                            }
                        }

                        List<Classes.Author> author = Classes.Author.AllAuthors.FindAll(x => x.FIO.Contains(book[2]));
                        if (author.Count == 0)
                        {
                            Classes.Author newAuthor = new Classes.Author(Classes.Author.AllAuthors.Last().Id + 1, book[2]);
                            Classes.Author.AllAuthors.Add(newAuthor);
                        }

                        Classes.Book newBook = new Classes.Book(Classes.Book.AllBooks.Last().Id + 1, book[0],
                            Classes.Genre.AllGenres.FindAll(x => book_genre.Any(y => y.Contains(x.Name))),
                            Classes.Author.AllAuthors.FindAll(x => x.FIO.Contains(book[2])), int.Parse(book[3]));
                        Classes.Book.AllBooks.Add(newBook);
                    }
                    sr.Close();
                    MessageBox.Show("Данные успешно загружены :)", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
            }
            catch
            {
                MessageBox.Show("Проверьте правильность ввода директории.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }
    }
}
