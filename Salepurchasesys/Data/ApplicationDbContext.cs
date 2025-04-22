using Microsoft.EntityFrameworkCore;
using SalePurchasesys.Models;
using SalePurchasesys.Services;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SalePurchasesys.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets for each model/table in the database
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductSubCategory> ProductSubCategories { get; set; }

        // Override the OnModelCreating method to configure relationships if necessary
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationships between tables (if needed)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductSubCategory)
                .WithMany(ps => ps.Products)
                .HasForeignKey(p => p.ProductSubCategoryId);

            modelBuilder.Entity<SaleDetail>()
                .HasOne(sd => sd.Sale)
                .WithMany(s => s.SaleDetails)
                .HasForeignKey(sd => sd.SaleId);

            modelBuilder.Entity<SaleDetail>()
                .HasOne(sd => sd.Product)
                .WithMany(p => p.SaleDetails)
                .HasForeignKey(sd => sd.ProductId);

            modelBuilder.Entity<PurchaseDetail>()
       .HasOne(pd => pd.Purchase)
       .WithMany(p => p.PurchaseDetails)
       .HasForeignKey(pd => pd.PurchaseId)
       .OnDelete(DeleteBehavior.Cascade); // Or DeleteBehavior.SetNull if needed

            modelBuilder.Entity<PurchaseDetail>()
                .HasOne(pd => pd.Product)
                .WithMany(p => p.PurchaseDetails)
                .HasForeignKey(pd => pd.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductSubCategory>()
                .HasOne(psc => psc.ProductCategory)
                .WithMany(pc => pc.ProductSubCategories)
                .HasForeignKey(psc => psc.ProductCategoryId);
        }
    }
}
