using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalePurchasesys.Data;
using SalePurchasesys.Models;
using SalePurchasesys.DTOs;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalePurchasesys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductCategoryController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetCategories()
        {
            var categories = await _context.ProductCategories.ToListAsync();
            return Ok(_mapper.Map<List<ProductCategoryDto>>(categories));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategoryDto>> GetCategory(int id)
        {
            var category = await _context.ProductCategories.FindAsync(id);
            if (category == null) return NotFound();

            return Ok(_mapper.Map<ProductCategoryDto>(category));
        }

        [HttpPost]
        public async Task<ActionResult<ProductCategoryDto>> CreateCategory(ProductCategoryDto categoryDto)
        {
            var category = _mapper.Map<ProductCategory>(categoryDto);
            _context.ProductCategories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, _mapper.Map<ProductCategoryDto>(category));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, ProductCategoryDto categoryDto)
        {
            if (id != categoryDto.Id)
                return BadRequest("Category ID mismatch.");

            var category = _mapper.Map<ProductCategory>(categoryDto);
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.ProductCategories.FindAsync(id);
            if (category == null) return NotFound();

            _context.ProductCategories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
