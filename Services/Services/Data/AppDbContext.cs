using Microsoft.EntityFrameworkCore;
using Services.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Services.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Local> Locals { get; set; }
        public DbSet<MenuTheme> MenuThemes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CategoryProduct>()
                .HasKey(cp => new { cp.CategoriesId, cp.ProductsId });

            modelBuilder.Entity<CategoryProduct>()
                .HasOne(cp => cp.Category)
                .WithMany(c => c.CategoryProducts)
                .HasForeignKey(cp => cp.CategoriesId);

            modelBuilder.Entity<CategoryProduct>()
                .HasOne(cp => cp.Product)
                .WithMany(p => p.CategoryProducts)
                .HasForeignKey(cp => cp.ProductsId);

            modelBuilder.Entity<CategoryProduct>()
                .ToTable("ProductCategories");
        }

    }
}
