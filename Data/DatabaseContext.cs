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

                //Database.EnsureDeleted();
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

            builder.Entity<FactorItem>()
                .HasOne(fi => fi.Product)
                .WithMany()
                .HasForeignKey(fi => fi.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Store>()
                .HasMany(s => s.Factors)
                .WithOne(f => f.Store)
                .HasForeignKey(f => f.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

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

            builder.Entity<User>()
                .HasMany(u => u.Stores)
                .WithOne(s => s.Owner)
                .HasForeignKey(s => s.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<User>()
                .HasOne(a => a.RefreshToken)
                .WithOne(b => b.User)
                .HasForeignKey<UserRefreshToken>(b => b.OwnerId);


            builder.Entity<ImageAsset>(entity =>
            {
                entity.HasOne(a => a.Owner)
                    .WithMany(i => i.ImageAssets)
                    .HasForeignKey(a => a.OwnerId);
            });

            builder.Entity<Product>()
            .HasOne(c => c.Store)
            .WithMany(s => s.Products)
            .HasForeignKey(x => x.StoreId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Category>()
            .HasOne(c => c.Store)
            .WithMany(s => s.Categories)
            .HasForeignKey(x => x.StoreId)
            .OnDelete(DeleteBehavior.NoAction);



            Role programmerRole = new Role()
            {
                Id = Guid.NewGuid(),
                Name = "Programmer",
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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RoleActionPermission> RoleActionPermission { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ActionPermission> ActionPermissions { get; set; }
        public DbSet<ImageAsset> ImageAssets { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
    }
}
