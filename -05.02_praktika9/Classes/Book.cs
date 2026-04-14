using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktika9.Classes
{
    public class Book
    {
        ///<summary>Код книги</summary>
        public int Id { get; set; }
        ///<summary>Наименование</summary>
        public string Name { get; set; }
        ///<summary>Жанры</summary>
        public List<Genre> Genres { get; set; }
        ///<summary>Авторы</summary>
        public List<Author> Authors { get; set; }
        ///<summary>Год издания</summary>
        public int Year { get; set; }
        ///<summary>Конструктор книг</summary>
        public Book (int Id, string Name, List<Genre> Genres, List<Author> Authors, int Year)
        {
            this.Id = Id;
            this.Name = Name;
            this.Genres = Genres;
            this.Authors = Authors;
            this.Year = Year;
        }

        public static List<Book> AllBooks = new List<Book>();

        ///<summary>Репозиторий книг</summary>
        public static List<Book> RepoBooks()
        {
            AllBooks.Add(new Book(
                1, "Путешествие в Элевсин",
                Genre.AllGenres.FindAll(x => x.Id == 1),
                Author.AllAuthors.FindAll(x => x.Id == 1), 2023));
            AllBooks.Add(new Book(
                2, "Чапаев и пустота",
                Genre.AllGenres.FindAll(x => x.Id == 1),
                Author.AllAuthors.FindAll(x => x.Id == 1), 2008));
            AllBooks.Add(new Book(
                3, "Дебютная постановка. Том 1.",
                Genre.AllGenres.FindAll(x => x.Id == 2),
                Author.AllAuthors.FindAll(x => x.Id == 2), 2023));
            AllBooks.Add(new Book(
                4, "Дебютная постановка. Том 2.",
                Genre.AllGenres.FindAll(x => x.Id == 2),
                Author.AllAuthors.FindAll(x => x.Id == 2), 2023));
            AllBooks.Add(new Book(
                5, "Охота на попаданку. Бракованная жена.",
                Genre.AllGenres.FindAll(x => x.Id == 2 || x.Id == 3 || x.Id == 4),
                Author.AllAuthors.FindAll(x => x.Id == 3), 2022));

            return AllBooks;
        }

        ///<summary>Список жанров через ряпятую</summary>
        public string ToGenres()
        {
            string toGenres = "";
            
            for (int iGenre = 0; iGenre < this.Genres.Count; iGenre++)
            {
                toGenres += this.Genres[iGenre].Name;
                if (iGenre < this.Genres.Count - 1) toGenres += ", ";
            }

            return toGenres;
        }
        ///<summary>Список авторов через запятую</summary>
        public string ToAuthors()
        {
            string toAuthors = "";

            for(int iAuthor = 0; iAuthor < this.Authors.Count; iAuthor++)
            {
                toAuthors += this.Authors[iAuthor].FIO;
                if(iAuthor < this.Authors.Count - 1) toAuthors += ", ";
            }
            return toAuthors;
        }
    }
}
