using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using praktika9.SecondWindows;
using praktika9.Classes;

namespace praktika9
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool Updating = true;

        public MainWindow()
        {
            InitializeComponent();
            
            // Инициализируем наши репозитории
            Genre.RepoGenres();
            Author.RepoAuthors();
            Book.RepoBooks();

            AddAuthors();
            AddGenres();
            AddYears();

            CreateUI(Classes.Book.AllBooks);
        }

        public void CreateUI(List<Classes.Book> AllBooks)
        {
            // Очищаем холст
            Parent.Children.Clear();

            foreach (Classes.Book Book in AllBooks) 
                Parent.Children.Add(new Elements.BookInfo(Book));
        }

        public void AddAuthors()
        {
            cbAuthors.Items.Clear();
            // Доб. пункт выберите
            List<Author> AllAuthors = Classes.Author.AllAuthors;
            cbAuthors.Items.Add("Выберите ...");
            foreach (Classes.Author Author in AllAuthors)
                cbAuthors.Items.Add(Author.FIO);
        }

        public void AddGenres()
        {
            cbGenres.Items.Clear();
            List<Genre> AllGenres = Classes.Genre.AllGenres;
            cbGenres.Items.Add("Выберите ...");
            foreach (Classes.Genre Genre in AllGenres)
                cbGenres.Items.Add(Genre.Name);
        }

        public void AddYears()
        {
            cbYear.Items.Clear();
            cbYear.Items.Add("Выберите ...");

            List<int> AllYears = new List<int>(); // Создаём коллекцию из годов

            List<Book> AllBooks = Classes.Book.AllBooks;
            foreach (Classes.Book Book in AllBooks)
            {
                // Если в коллекции нет нашего года
                if (AllYears.Find(x => x == Book.Year) == 0)
                {
                    AllYears.Add(Book.Year); // Добавляем год в коллекцию
                    cbYear.Items.Add(Book.Year); // Добавляем в выпадающий список
                }
            }
        }

        private void Search_Book(object sender, KeyEventArgs e) => Search();

        public void Search()
        {
            if (Updating == false) { MessageBox.Show("Обновите данные.", "Внимание!"); return; }

            // Ищем книги при поиске
            List<Classes.Book> FindBook = Classes.Book.AllBooks.FindAll(x => x.Name.ToLower().Contains(tbSearch.Text.ToLower()));

            // Если выбран автор
            if (cbAuthors.SelectedIndex > 0)
            {
                // Ищем выбранного автора
                Classes.Author SelectAuthor = Classes.Author.AllAuthors.Find(x => x.FIO == cbAuthors.SelectedItem.ToString());
                // Ищем выбранные книги
                FindBook = FindBook.FindAll(x => x.Authors.Find(y => y.Id == SelectAuthor.Id) != null);
            }

            // Если выбран жанр
            if (cbGenres.SelectedIndex > 0)
            {
                // Ищем выбранный жанр
                Classes.Genre SelectGender = Classes.Genre.AllGenres.Find(x => x.Name == cbGenres.SelectedItem.ToString());
                // Ищем выбраные книги
                FindBook = FindBook.FindAll(x => x.Genres.Find(y => y.Id == SelectGender.Id) != null);
            }

            //Если выбран год
            if(cbYear.SelectedIndex > 0)
                FindBook = FindBook.FindAll(x => x.Year == Convert.ToInt32(cbYear.SelectedItem.ToString())); // Ищем выбранные книги

            // генерируем интерфейс
            CreateUI(FindBook);
        }

        private void SelectAuthor(object sender, SelectionChangedEventArgs e) => Search();

        private void save_repo(object sender, RoutedEventArgs e)
        {
            Saving save_window = new Saving();
            save_window.Show();
        }

        private void upload_repo(object sender, RoutedEventArgs e)
        {
            Upload upload_window = new Upload();
            upload_window.Show();
            Updating = false;
        }

        private void update(object sender, RoutedEventArgs e)
        {
            Updating = true;
            AddAuthors();
            AddGenres();
            AddYears();
            CreateUI(Classes.Book.AllBooks);
        }
    }
}
