using System;

namespace praktika20.Models
{
    public class Work
    {
        public int Id { get; set; }
        public int IdDescipline { get; set; }
        public int IdType { get; set; }
        public DateTime Date {  get; set; }
        public string Name { get; set; }
        public int Semester {  get; set; }

        public Work (int id, int idDescipline, int idType, DateTime date, string name, int semester)
        {
            Id = id;
            IdDescipline = idDescipline;
            IdType = idType;
            Date = date;
            Name = name;
            Semester = semester;
        }
    }
}
