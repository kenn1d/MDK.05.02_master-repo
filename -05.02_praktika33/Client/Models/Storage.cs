namespace Client.Models
{
    /// <summary>
    /// Хранилище паролей
    /// </summary>
    public class Storage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Login {  get; set; }
        public string Password { get; set; }
    }
}
