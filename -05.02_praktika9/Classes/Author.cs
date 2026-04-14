using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktika9.Classes
{
    public class Author
    {
        ///<summary>Код автора</summary>
        public int Id { get; set; }
        ///<summary>ФИО автора</summary>
        public string FIO { get; set; }
        ///<summary>Конструктор автора</summary>
        public Author (int Id, string FIO)
        {
            this.Id = Id;
            this.FIO = FIO;
        }

        public static List<Author> AllAuthors = new List<Author>();

        ///<summary>Репозиторий авторов</summary>
        public static List<Author> RepoAuthors()
        {
            if (AllAuthors.Count == 0) {
                AllAuthors.Add(new Author(1, "Виктор Пелевин"));
                AllAuthors.Add(new Author(2, "Александра Маринина"));
                AllAuthors.Add(new Author(3, "Ольга Герр"));
            }

            return AllAuthors;
        }
    }
}
