namespace WpfApp1.Model
{
    public class Bilet
    {
        public int Id { get; set; }
        public int idAfisha { get; set; }

        public Bilet(int Id, int idAfisha) { 
            this.Id = Id;
            this.idAfisha = idAfisha;
        }
    }
}
