using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktika4.Classes
{
    public class Student
    {
        public string Firstname = "";
        public string Lastname = "";
        public string Surname = "";
        public bool Scholarship = false;
        public int Course = 4;
        public string href = "";

        // Принимает фио
        public Student(string Firstname, string Lastname, string Surname, string href)
        {
            this.Firstname = Firstname;
            this.Lastname = Lastname;
            this.Surname = Surname;
            this.href = href;
        }

        // Принимает фио и стипендию
        public Student(string Firstname, string Lastname, string Surname, string href, bool Scholarship) 
            : this(Firstname, Lastname, Surname, href) {
            this.Scholarship = Scholarship;
        }

        // Принимает ФИО стипендию и курс
        public Student(string Firstname, string Lastname, string Surname, string href, bool Scholarship, int Course)
            : this(Firstname, Lastname, Surname, href, Scholarship) {
            this.Course = Course;
        }

        // Возвращает склеенное фио
        public string GetFIO()
        {
            return $"{Firstname} {Lastname} {Surname}";
        }
    }
}
