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
    public class ProductSubCategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductSubCategoryController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductSubCategoryDto>>> GetSubCategories()
        {
            var subCategories = await _context.ProductSubCategories
                .Include(sc => sc.ProductCategory)
                .ToListAsync();

            return Ok(_mapper.Map<List<ProductSubCategoryDto>>(subCategories));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductSubCategoryDto>> GetSubCategory(int id)
        {
            var subCategory = await _context.ProductSubCategories
                .Include(sc => sc.ProductCategory)
                .FirstOrDefaultAsync(sc => sc.Id == id);

            if (subCategory == null)
                return NotFound();

            return Ok(_mapper.Map<ProductSubCategoryDto>(subCategory));
        }

        [HttpPost]
        public async Task<ActionResult<ProductSubCategoryDto>> CreateSubCategory(ProductSubCategoryDto createDto)
        {
            var subCategory = _mapper.Map<ProductSubCategory>(createDto);
            _context.ProductSubCategories.Add(subCategory);
            await _context.SaveChangesAsync();

            var subCategoryDto = _mapper.Map<ProductSubCategoryDto>(subCategory);
            return CreatedAtAction(nameof(GetSubCategory), new { id = subCategory.Id }, subCategoryDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubCategory(int id, ProductSubCategoryDto subCategoryDto)
        {
            if (id != subCategoryDto.Id)
                return BadRequest("SubCategory ID mismatch.");

            var subCategory = _mapper.Map<ProductSubCategory>(subCategoryDto);
            _context.Entry(subCategory).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            var subCategory = await _context.ProductSubCategories.FindAsync(id);
            if (subCategory == null) return NotFound();

            _context.ProductSubCategories.Remove(subCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
