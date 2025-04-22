using Microsoft.AspNetCore.Mvc;
using SalePurchasesys.Models;
using SalePurchasesys.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalePurchasesys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSubCategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductSubCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductSubCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductSubCategory>>> GetSubCategories()
        {
            // Including the parent ProductCategory for reference
            return await _context.ProductSubCategories.Include(sc => sc.ProductCategory).ToListAsync();
        }

        // GET: api/ProductSubCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductSubCategory>> GetSubCategory(int id)
        {
            var subCategory = await _context.ProductSubCategories
                .Include(sc => sc.ProductCategory)
                .FirstOrDefaultAsync(sc => sc.Id == id);
            if (subCategory == null)
                return NotFound();
            return subCategory;
        }

        // POST: api/ProductSubCategory
        [HttpPost]
        public async Task<ActionResult<ProductSubCategory>> CreateSubCategory(ProductSubCategory subCategory)
        {
            _context.ProductSubCategories.Add(subCategory);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSubCategory), new { id = subCategory.Id }, subCategory);
        }

        // PUT: api/ProductSubCategory/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubCategory(int id, ProductSubCategory subCategory)
        {
            if (id != subCategory.Id)
                return BadRequest("SubCategory ID mismatch.");

            _context.Entry(subCategory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/ProductSubCategory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            var subCategory = await _context.ProductSubCategories.FindAsync(id);
            if (subCategory == null)
                return NotFound();

            _context.ProductSubCategories.Remove(subCategory);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
