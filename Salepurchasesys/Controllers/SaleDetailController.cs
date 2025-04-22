using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalePurchasesys.Data;
using SalePurchasesys.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalePurchasesys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleDetailController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SaleDetailController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SaleDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDetail>>> GetSaleDetails()
        {
            var saleDetails = await _context.Set<SaleDetail>()
                .Include(sd => sd.Sale)
                .Include(sd => sd.Product)
                .ToListAsync();
            return Ok(saleDetails);
        }

        // GET: api/SaleDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDetail>> GetSaleDetail(int id)
        {
            var saleDetail = await _context.Set<SaleDetail>()
                .Include(sd => sd.Sale)
                .Include(sd => sd.Product)
                .FirstOrDefaultAsync(sd => sd.Id == id);
            if (saleDetail == null)
                return NotFound();
            return Ok(saleDetail);
        }

        // POST: api/SaleDetail
        [HttpPost]
        public async Task<ActionResult<SaleDetail>> CreateSaleDetail([FromBody] SaleDetail saleDetail)
        {
            if (saleDetail == null || saleDetail.ProductId == 0 || saleDetail.Quantity == 0)
            {
                return BadRequest("SaleDetail must have a valid ProductId and Quantity.");
            }

            // Check if Sale exists; if not, create a new Sale
            var sale = await _context.Sales.FindAsync(saleDetail.SaleId);
            if (sale == null)
            {
                sale = new Sale
                {
                    OrderDate = DateTime.UtcNow // Set current date
                };
                _context.Sales.Add(sale);
                await _context.SaveChangesAsync();

                // Assign the newly created Sale ID to SaleDetail
                saleDetail.SaleId = sale.Id;
            }

            // Check if Product exists
            var product = await _context.Products.FindAsync(saleDetail.ProductId);
            if (product == null)
            {
                return NotFound($"Product with ID {saleDetail.ProductId} not found.");
            }

            // Calculate Subtotal if needed
            saleDetail.CalculateSubtotal(product.Price);

            _context.Add(saleDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSaleDetail), new { id = saleDetail.Id }, saleDetail);
        }

        // PUT: api/SaleDetail/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSaleDetail(int id, [FromBody] SaleDetail saleDetail)
        {
            if (id != saleDetail.Id)
                return BadRequest("SaleDetail ID mismatch.");

            _context.Entry(saleDetail).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/SaleDetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaleDetail(int id)
        {
            var saleDetail = await _context.Set<SaleDetail>().FindAsync(id);
            if (saleDetail == null)
                return NotFound();
            _context.Remove(saleDetail);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
