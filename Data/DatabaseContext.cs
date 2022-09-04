using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext
            (DbContextOptions<DatabaseContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();

            //Database.Migrate();
        }

        private readonly DbContextOptions options;

        private DbContextOptions GetOptions()
        {
            return options;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Factor> Factors { get; set; }
        public DbSet<FactorItem> FactorItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
