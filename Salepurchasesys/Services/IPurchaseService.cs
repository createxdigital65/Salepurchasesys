using SalePurchasesys.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalePurchasesys.Services
{
    public interface IPurchaseService
    {
        Task<IEnumerable<PurchaseDto>> GetAllPurchasesAsync();
        Task<PurchaseDto> GetPurchaseByIdAsync(int id);
        Task<PurchaseDto> CreatePurchaseAsync(CreatePurchaseDto createDto);
        Task<PurchaseDto> UpdatePurchaseAsync(int id, UpdatePurchaseDto updateDto);
        Task<bool> DeletePurchaseAsync(int id);
    }
}
