using SalePurchasesys.Models;
using SalePurchasesys.Services;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SalePurchasesys.Models
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<ProductSubCategory> ProductSubCategories { get; set; } = new List<ProductSubCategory>();
    }
}
    