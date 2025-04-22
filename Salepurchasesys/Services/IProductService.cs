using SalePurchasesys.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalePurchasesys.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();  // Fetch all products
        Task<Product> GetProductByIdAsync(int id);  // Fetch product by Id
        Task<Product> CreateProductAsync(Product product);  // Create new product
        Task<Product> UpdateProductAsync(int id, Product product);  // Update product details
        Task<bool> DeleteProductAsync(int id);  // Delete product
    }
}
