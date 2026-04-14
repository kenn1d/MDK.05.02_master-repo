namespace praktika26.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public DateTime RentStart { get; set; }
        public int Duration { get; set; }
        public int IdClub { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
