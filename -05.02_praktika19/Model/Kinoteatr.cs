
namespace WpfApp1.Model
{
    public class Kinoteatr
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public int CountZal { get; set; }
        public int Count { get; set; }

        public Kinoteatr(int Id, string Name, int CountZal, int Count) {
            this.Id = Id;
            this.Name = Name;
            this.CountZal = CountZal;
            this.Count = Count;
        }
    }
}
