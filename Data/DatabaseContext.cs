using Common;
using Microsoft.EntityFrameworkCore;
using Models;
using System;

namespace Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext
            (DbContextOptions<DatabaseContext> options) : base(options)
        {
            try
            {

                Database.EnsureDeleted();
                Database.EnsureCreated();

                //Database.Migrate();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private readonly DbContextOptions options;

        private DbContextOptions GetOptions()
        {
            return options;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Factor>()
                .HasMany(f => f.FactorItems)
                .WithOne(fi => fi.Factor)
                .HasForeignKey(fi => fi.FactorId);

            builder.Entity<RoleActionPermission>()
                .HasKey(ra => new { ra.ActionPermissionId, ra.RoleId });

            builder.Entity<RoleActionPermission>()
                .HasOne(ra => ra.ActionPermission)
                .WithMany(ap => ap.RoleActionPermission)
                .HasForeignKey(bc => bc.ActionPermissionId);

            builder.Entity<RoleActionPermission>()
                .HasOne(r => r.Role)
                .WithMany(ap => ap.RoleActionPermissions)
                .HasForeignKey(bc => bc.RoleId);


            Role programmerRole = new Role()
            {
                Id  = Guid.NewGuid(),
                RolName = "Programmer",
                Description = "Full access Programmer.",
                RoleActionPermissions = null
            };
            builder.Entity<Role>().HasData(new[] { programmerRole });

            User user = new User()
            {
                UserName = "Programmer",
                Password = Utilities.HashSHA1("prog123"),
                FirstName = "Programmer",
                LastName = "Programmer",
                IsActive = true,
                NationalCode = "00000000",
                RoleId = programmerRole.Id,
            };
            builder.Entity<User>().HasData(new[] { user });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }


        public DbSet<Customer> Customers { get; set; }
        public DbSet<Factor> Factors { get; set; }
        public DbSet<FactorItem> FactorItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RoleActionPermission> RoleActionPermission { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ActionPermission> ActionPermissions { get; set; }

    }
}
