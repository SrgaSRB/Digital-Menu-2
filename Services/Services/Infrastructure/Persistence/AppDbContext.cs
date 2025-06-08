using Microsoft.EntityFrameworkCore;
using Services.Domain.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Services.Infrastructure.Data
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
        public DbSet<CategorySubcategory> CategorySubcategories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategorySubcategory>()
                .HasKey(cs => new { cs.CategoryId, cs.SubcategoryId });

            modelBuilder.Entity<CategorySubcategory>()
                .HasOne(cs => cs.Category)
                .WithMany(c => c.SubcategoryLinks)
                .HasForeignKey(cs => cs.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CategorySubcategory>()
                .HasOne(cs => cs.Subcategory)
                .WithMany(sc => sc.CategoryLinks)
                .HasForeignKey(cs => cs.SubcategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubcategoryProduct>()
                .HasKey(sp => new { sp.SubcategoryId, sp.ProductId });

            modelBuilder.Entity<SubcategoryProduct>()
                .HasOne(sp => sp.Subcategory)
                .WithMany(sc => sc.SubcategoryProducts)
                .HasForeignKey(sp => sp.SubcategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubcategoryProduct>()
                .HasOne(sp => sp.Product)
                .WithMany(p => p.SubcategoryProducts)
                .HasForeignKey(sp => sp.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Subcategory>()
                .HasOne(sc => sc.Local)
                .WithMany(l => l.Subcategories)
                .HasForeignKey(sc => sc.LocalId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }


    }
}
