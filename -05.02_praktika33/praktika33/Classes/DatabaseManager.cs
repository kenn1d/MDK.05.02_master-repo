using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using praktika33.Models;

namespace praktika33.Classes
{
    public class DatabaseManager : DbContext
    {
        public DbSet<Storage> Storages { get; set; }
        public DbSet<User> Users { get; set; }
        public DatabaseManager() =>
            Database.EnsureCreated(); // - создаёт БД, если она не существует

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Настройка подключения к mysql БД
            optionsBuilder.UseMySql(
                "server=localhost;uid=root;pwd=;database=Storage;",
                new MySqlServerVersion(new Version(8, 0, 11)));
        }
    }
}
