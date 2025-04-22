using SalePurchasesys.Services;
using SalePurchasesys.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalePurchasesys.Data;

namespace SalePurchasesys.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly ApplicationDbContext _context;

        public PurchaseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchasesAsync()
        {
            return await _context.Purchases.ToListAsync();
        }

        public async Task<Purchase> GetPurchaseByIdAsync(int id)
        {
            return await _context.Purchases.FindAsync(id);
        }

        public async Task<Purchase> CreatePurchaseAsync(Purchase purchase)
        {
            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();
            return purchase;
        }

        public async Task<Purchase> UpdatePurchaseAsync(int id, Purchase purchase)
        {
            var existingPurchase = await _context.Purchases.FindAsync(id);
            if (existingPurchase == null)
                return null;

            existingPurchase.UserId = purchase.UserId;
            existingPurchase.PurchaseDate = purchase.PurchaseDate;
            existingPurchase.TotalAmount = purchase.TotalAmount;

            await _context.SaveChangesAsync();
            return existingPurchase;
        }

        public async Task<bool> DeletePurchaseAsync(int id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null)
                return false;

            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
