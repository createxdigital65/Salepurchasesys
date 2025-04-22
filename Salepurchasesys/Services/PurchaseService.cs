using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalePurchasesys.Data;
using SalePurchasesys.DTOs;
using SalePurchasesys.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalePurchasesys.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PurchaseService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PurchaseDto>> GetAllPurchasesAsync()
        {
            var purchases = await _context.Purchases.ToListAsync();
            return _mapper.Map<IEnumerable<PurchaseDto>>(purchases);
        }

        public async Task<PurchaseDto> GetPurchaseByIdAsync(int id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            return purchase == null ? null : _mapper.Map<PurchaseDto>(purchase);
        }

        public async Task<PurchaseDto> CreatePurchaseAsync(CreatePurchaseDto createDto)
        {
            var purchase = _mapper.Map<Purchase>(createDto);
            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();
            return _mapper.Map<PurchaseDto>(purchase);
        }

        public async Task<PurchaseDto> UpdatePurchaseAsync(int id, UpdatePurchaseDto updateDto)
        {
            var existing = await _context.Purchases.FindAsync(id);
            if (existing == null) return null;

            _mapper.Map(updateDto, existing);
            await _context.SaveChangesAsync();
            return _mapper.Map<PurchaseDto>(existing);
        }

        public async Task<bool> DeletePurchaseAsync(int id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null) return false;

            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
