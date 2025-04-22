using SalePurchasesys.Services;
using SalePurchasesys.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalePurchasesys.Services
{
    public interface IPurchaseService
    {
        Task<IEnumerable<Purchase>> GetAllPurchasesAsync();
        Task<Purchase> GetPurchaseByIdAsync(int id);
        Task<Purchase> CreatePurchaseAsync(Purchase purchase);
        Task<Purchase> UpdatePurchaseAsync(int id, Purchase purchase);
        Task<bool> DeletePurchaseAsync(int id);
    }
}
