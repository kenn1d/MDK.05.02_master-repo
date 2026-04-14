namespace praktika20.Models
{
    public class Evaluation
    {
        public int Id { get; set; }
        public int IdWork {  get; set; }
        public int IdStudent { get; set; }
        public string Value { get; set; }
        public string Lateness { get; set; }

        public Evaluation (int id, int idWork, int idStudent, string value, string lateness)
        {
            Id = id;
            IdWork = idWork;
            IdStudent = idStudent;
            Value = value;
            Lateness = lateness;
        }
    }
}
