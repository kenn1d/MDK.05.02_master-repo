namespace praktika20.Models
{
    public class Discipline
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdGroup { get; set; }

        public Discipline (int id, string name, int idGroup)
        {
            Id = id;
            Name = name;
            IdGroup = idGroup;
        }
    }
}
