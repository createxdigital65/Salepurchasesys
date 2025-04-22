using SalePurchasesys.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalePurchasesys.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(ProductCreateDto productDto);
        Task<ProductDto> UpdateProductAsync(int id, ProductUpdateDto productDto);
        Task<bool> DeleteProductAsync(int id);
    }
}
