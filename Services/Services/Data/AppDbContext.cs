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
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<SubcategoryProduct> SubcategoryProducts { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubcategoryProduct>()
                .HasKey(sp => new { sp.SubcategoryId, sp.ProductId });

            modelBuilder.Entity<SubcategoryProduct>()
                .HasOne(sp => sp.Subcategory)
                .WithMany(s => s.SubcategoryProducts)
                .HasForeignKey(sp => sp.SubcategoryId);

            modelBuilder.Entity<SubcategoryProduct>()
                .HasOne(sp => sp.Product)
                .WithMany(p => p.SubcategoryProducts)
                .HasForeignKey(sp => sp.ProductId);
        }

    }
}
