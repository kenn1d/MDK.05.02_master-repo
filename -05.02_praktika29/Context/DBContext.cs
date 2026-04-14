using Microsoft.EntityFrameworkCore;
using praktika29.Models;

namespace praktika29.Context
{
    public class DBContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DBContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().Ignore(e => e.IsEnable);
            modelBuilder.Entity<Product>().Ignore(e => e.IsEnable);
            modelBuilder.Entity<Order>().Ignore(e => e.IsEnable);

            base.OnModelCreating(modelBuilder);
        }
    }
}
