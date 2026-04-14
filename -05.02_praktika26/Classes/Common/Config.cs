using Microsoft.EntityFrameworkCore;

namespace praktika26.Classes.Common
{
    public class Config
    {
        public static string ConnectionConfig = "server=localhost;uid=root;pwd=;database=pcClub;";
        /// <summary>
        /// Версия MySql сервера при подключении
        /// </summary>
        public static MySqlServerVersion Version = new MySqlServerVersion(new System.Version(8, 0, 11));
    }
}
