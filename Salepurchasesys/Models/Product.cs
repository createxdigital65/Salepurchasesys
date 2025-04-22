using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalePurchasesys.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int UserId { get; set; }

        // Foreign Key for ProductSubCategory
        public int ProductSubCategoryId { get; set; }

        // Navigation Property (make it nullable to avoid required validation error)
        [ForeignKey("ProductSubCategoryId")]
        public ProductSubCategory? ProductSubCategory { get; set; }

        // Initialize collections to prevent null reference exceptions
        public ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
        public ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();
    }
}
