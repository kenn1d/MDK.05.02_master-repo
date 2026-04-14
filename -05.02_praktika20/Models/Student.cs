using System;

namespace praktika20.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IdGroup { get; set; }
        public bool Expelled { get; set; }
        public DateTime DateExpelled { get; set; }

        public Student (int id, string firstName, string lastName, int idGroup, bool expelled, DateTime dateExpelled)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            IdGroup = idGroup;
            Expelled = expelled;
            DateExpelled = dateExpelled;
        }
    }
}
