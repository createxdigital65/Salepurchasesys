using SalePurchasesys.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using SalePurchasesys.Data;
using AutoMapper;
using SalePurchasesys.DTOs;

namespace SalePurchasesys.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            return product == null ? null : _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateProductAsync(ProductCreateDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> UpdateProductAsync(int id, ProductUpdateDto productDto)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null) return null;

            _mapper.Map(productDto, existingProduct); // updates the existing product
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDto>(existingProduct);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
