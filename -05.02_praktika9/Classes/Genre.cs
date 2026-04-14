using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktika9.Classes
{
    public class Genre
    {
        ///<summary>Код жанра</summary>
        public int Id { get; set; }
        ///<summary>Наименование жанра</summary>
        public string Name { get; set; }
        ///<summary>Конструктор для жанров</summary>
        public Genre (int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public static List<Genre> AllGenres = new List<Genre>();

        ///<summary>Репозиторий жанров</summary>
        public static List<Genre> RepoGenres()
        {
            if (AllGenres.Count == 0)
            {
                AllGenres.Add(new Genre(1, "Современная русская литература"));
                AllGenres.Add(new Genre(2, "Современный детективы"));
                AllGenres.Add(new Genre(3, "Любовное фэнтези"));
                AllGenres.Add(new Genre(4, "Попаданцы"));
                AllGenres.Add(new Genre(5, "Юмористическое фэнтези"));
            }
            return AllGenres;
        }
    }
}
