using Microsoft.EntityFrameworkCore;
using SalePurchasesys.Models;

namespace SalePurchasesys.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductSubCategory> ProductSubCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .ToTable("Products")
                .HasOne(p => p.ProductSubCategory)
                .WithMany(ps => ps.Products)
                .HasForeignKey(p => p.ProductSubCategoryId);

            modelBuilder.Entity<SaleDetail>()
                .ToTable("SaleDetails")
                .HasOne(sd => sd.Sale)
                .WithMany(s => s.SaleDetails)
                .HasForeignKey(sd => sd.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SaleDetail>()
                .HasOne(sd => sd.Product)
                .WithMany(p => p.SaleDetails)
                .HasForeignKey(sd => sd.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PurchaseDetail>()
                .ToTable("PurchaseDetails")
                .HasOne(pd => pd.Purchase)
                .WithMany(p => p.PurchaseDetails)
                .HasForeignKey(pd => pd.PurchaseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PurchaseDetail>()
                .HasOne(pd => pd.Product)
                .WithMany(p => p.PurchaseDetails)
                .HasForeignKey(pd => pd.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductSubCategory>()
                .ToTable("ProductSubCategories")
                .HasOne(psc => psc.ProductCategory)
                .WithMany(pc => pc.ProductSubCategories)
                .HasForeignKey(psc => psc.ProductCategoryId);
        }
    }
}
