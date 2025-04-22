using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalePurchasesys.Data;
using SalePurchasesys.DTOs;
using SalePurchasesys.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalePurchasesys.Services
{
    public class SaleService : ISaleService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SaleService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SaleDto>> GetAllSalesAsync()
        {
            var sales = await _context.Sales
                .Include(s => s.SaleDetails)
                .ToListAsync();

            return _mapper.Map<IEnumerable<SaleDto>>(sales);
        }

        public async Task<SaleDto> GetSaleByIdAsync(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.SaleDetails)
                .FirstOrDefaultAsync(s => s.Id == id);

            return sale == null ? null : _mapper.Map<SaleDto>(sale);
        }

        // ✅ Accepts Sale (not DTO)
        public async Task<Sale> CreateSaleAsync(Sale sale)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            return sale;
        }

        // ✅ Accepts Sale (not DTO)
        public async Task<Sale> UpdateSaleAsync(int id, Sale updatedSale)
        {
            var existingSale = await _context.Sales
                .Include(s => s.SaleDetails)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (existingSale == null) return null;

            // Update top-level sale properties
            _context.Entry(existingSale).CurrentValues.SetValues(updatedSale);

            // Optionally update SaleDetails logic can go here if needed
            // e.g., delete old, add new, or update existing ones

            await _context.SaveChangesAsync();
            return existingSale;
        }

        public async Task<bool> DeleteSaleAsync(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null) return false;

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }
    }
}
