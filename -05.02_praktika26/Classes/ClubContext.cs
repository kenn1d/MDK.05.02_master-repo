using Microsoft.EntityFrameworkCore;
using praktika26.Classes.Common;
using praktika26.Models;

namespace praktika26.Classes
{
    public class ClubContext : DbContext
    {
        /// <summary>
        /// Данные из БД
        /// </summary>
        public DbSet<Clubs> Clubs { get; set; }

        /// <summary>
        /// Конструктор для контекста
        /// </summary>
        public ClubContext() =>
            // Проверяем, создано и подключение, если не создано - создаём
            Database.EnsureCreated();

        /// <summary>
        /// Переопределённый метод конфигурации
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.ConnectionConfig, Config.Version);
        }
    }
}
