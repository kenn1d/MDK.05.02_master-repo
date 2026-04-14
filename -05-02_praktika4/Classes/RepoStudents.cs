using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace praktika4.Classes
{
    public class RepoStudents
    {
        ///<summary>Возвращает коллекцию студентов</summary>
        public static List<Student> AllStudent()
        {
            List<Student> allStudent = new List<Student>();

            allStudent.Add(new Student("Болотов", "Евгений", "Олегович", "/praktika4;component/Images/1.jpg"));
            allStudent.Add(new Student("Григорьев", "Роман", "Владимирович", "/praktika4;component/Images/2.jpg"));
            allStudent.Add(new Student("Гудков", "Георгий", "Константинович", "/praktika4;component/Images/3.jpg", false, 3));
            allStudent.Add(new Student("Исыпова", "Алёна", "Александровна", "/praktika4;component/Images/4.jpg", true));
            allStudent.Add(new Student("Иутин", "Павел", "Алексеевич", "/praktika4;component/Images/5.jpg", false, 3));
            allStudent.Add(new Student("Ишимов", "Виктор", "Алексеевич", "/praktika4;component/Images/1.jpg"));
            allStudent.Add(new Student("Калюжный", "Артём", "Евгеньевич", "/praktika4;component/Images/2.jpg"));
            allStudent.Add(new Student("Кусакина", "Полина", "Олеговна", "/praktika4;component/Images/3.jpg", true));
            allStudent.Add(new Student("Ленченков", "Александр", "Дмитриевич", "/praktika4;component/Images/4.jpg"));
            allStudent.Add(new Student("Лесникова", "Мария", "Михайловна", "/praktika4;component/Images/5.jpg", true));
            allStudent.Add(new Student("Лихачева", "Татьяна", "Яковлевна", "/praktika4;component/Images/1.jpg"));
            allStudent.Add(new Student("Мокрушина", "Надежда", "Владимирвна", "/praktika4;component/Images/2.jpg", true));
            allStudent.Add(new Student("Мутагаров", "Даниил", "Ринатович", "/praktika4;component/Images/3.jpg"));
            allStudent.Add(new Student("Нарижный", "Данил", "Валентинович", "/praktika4;component/Images/4.jpg"));
            allStudent.Add(new Student("Никонов", "Арсений", "Дмитриевич", "/praktika4;component/Images/5.jpg", false, 3));
            allStudent.Add(new Student("Оборин", "Данилл", "Артёмович", "/praktika4;component/Images/1.jpg"));
            allStudent.Add(new Student("Посадских", "Дарья", "Андреевна", "/praktika4;component/Images/2.jpg"));
            allStudent.Add(new Student("Сторожев", "Денис", "Романович", "/praktika4;component/Images/3.jpg", true));
            allStudent.Add(new Student("Суслов", "Егор", "Владимирович", "/praktika4;component/Images/4.jpg"));
            allStudent.Add(new Student("Токманов", "Даниил", "Сергеевич", "/praktika4;component/Images/5.jpg", true));
            allStudent.Add(new Student("Тронин", "Александр", "Владиславович", "/praktika4;component/Images/1.jpg"));
            allStudent.Add(new Student("Халилов", "Дамир", "Ринатович", "/praktika4;component/Images/2.jpg"));
            allStudent.Add(new Student("Шестаков", "Дмитрий", "Андреевич", "/praktika4;component/Images/3.jpg"));

            return allStudent;
        }

        public static List<Student> AddedStudent = new List<Student>();

        public List<Student> NewStud(string Firstname, string Lastname, string Surname, bool Scholarship, int Course)
        {
            if(Scholarship == false && Course == 4) AddedStudent.Add(new Student(Firstname, Lastname, Surname, "/praktika4;component/Images/ic_user.jpg"));
            else if (Scholarship == false && Course != 4) AddedStudent.Add(new Student(Firstname, Lastname, Surname, "/praktika4;component/Images/ic_user.jpg"));
            else if (Scholarship == true && Course != 4) AddedStudent.Add(new Student(Firstname, Lastname, Surname, "/praktika4;component/Images/ic_user.jpg", Scholarship, Course));
            else AddedStudent.Add(new Student(Firstname, Lastname, Surname, "/praktika4;component/Images/ic_user.jpg", Scholarship, Course));

            return AddedStudent;
        }
    }
}
