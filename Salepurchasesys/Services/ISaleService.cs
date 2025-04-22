using SalePurchasesys.DTOs;
using SalePurchasesys.Models; // ← Add this if not already there
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalePurchasesys.Services
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleDto>> GetAllSalesAsync();
        Task<SaleDto> GetSaleByIdAsync(int id);
        Task<Sale> CreateSaleAsync(Sale sale); // ✅ Changed
        Task<Sale> UpdateSaleAsync(int id, Sale sale); // ✅ Changed
        Task<bool> DeleteSaleAsync(int id);
        Task<Product> GetProductByIdAsync(int productId); // ✅ Use Product (not ProductDto)
    }
}
