using Microsoft.EntityFrameworkCore;

namespace praktika29.Context
{
    public class Config
    {
        public static readonly string connection = "server=localhost;uid=root;pwd=;database=ClientsAndProducts";

        public static readonly MySqlServerVersion version = new MySqlServerVersion(new Version(8, 0, 11));
    }
}
