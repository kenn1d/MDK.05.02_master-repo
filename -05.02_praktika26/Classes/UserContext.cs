using Microsoft.EntityFrameworkCore;
using praktika26.Classes.Common;
using praktika26.Models;

namespace praktika26.Classes
{
    public class UserContext : DbContext
    {
        public DbSet<Users> Users { get; set; }

        public UserContext() =>
            Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.ConnectionConfig, Config.Version);
        }
    }
}
