using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalePurchasesys.Models
{
    public class ProductSubCategory
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Subcategory Name is required.")]
        public string Name { get; set; }

        public string? Description { get; set; }  // Nullable to allow empty descriptions

        // Foreign Key for Product Category
        [Required]
        public int ProductCategoryId { get; set; }

        // Navigation Property for Product Category (ensure it's nullable to avoid circular dependency issues)
        public virtual ProductCategory? ProductCategory { get; set; }

        // Navigation Property for Products (one to many relationship)
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
