using Microsoft.EntityFrameworkCore;
using praktika28.Classes.Database;
using praktika28.Models;

namespace praktika28.Context
{
    public class TasksContext : DbContext
    {
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Sorted> Sorted { get; set; }
        public TasksContext()
        {
            // Создаём подключение
            Database.EnsureCreated();
            Tasks.Load();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Подключаемся к mysql со след. настройками из config
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sorted>().HasBaseType(null as Type);
            modelBuilder.Entity<Tasks>().ToTable("Tasks");
            modelBuilder.Entity<Sorted>().ToTable("Sorted");

            modelBuilder.Entity<Sorted>().Ignore(e => e.IsEnable);
            modelBuilder.Entity<Sorted>().Ignore(e => e.IsEnableText);
            modelBuilder.Entity<Sorted>().Ignore(e => e.IsDoneText);
            modelBuilder.Entity<Sorted>().Ignore(e => e.OnEdit);
            modelBuilder.Entity<Sorted>().Ignore(e => e.OnDelete);
            modelBuilder.Entity<Sorted>().Ignore(e => e.OnDone);

            base.OnModelCreating(modelBuilder);
        }
    }
}
