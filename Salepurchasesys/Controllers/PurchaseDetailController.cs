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
    public class PurchaseDetailController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PurchaseDetailController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PurchaseDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseDetail>>> GetPurchaseDetails()
        {
            return await _context.Set<PurchaseDetail>()
                .Include(pd => pd.Purchase)
                .Include(pd => pd.Product)
                .ToListAsync();
        }

        // GET: api/PurchaseDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseDetail>> GetPurchaseDetail(int id)
        {
            var purchaseDetail = await _context.Set<PurchaseDetail>()
                .Include(pd => pd.Purchase)
                .Include(pd => pd.Product)
                .FirstOrDefaultAsync(pd => pd.Id == id);
            if (purchaseDetail == null)
                return NotFound();
            return purchaseDetail;
        }

        // POST: api/PurchaseDetail
        [HttpPost]
        public async Task<ActionResult<PurchaseDetail>> CreatePurchaseDetail([FromBody] PurchaseDetail purchaseDetail)
        {
            if (purchaseDetail == null)
            {
                return BadRequest("PurchaseDetail data is null.");
            }

            // Ensure the PurchaseId and ProductId are valid
            var purchase = await _context.Purchases.FindAsync(purchaseDetail.PurchaseId);
            var product = await _context.Products.FindAsync(purchaseDetail.ProductId);

            if (purchase == null)
            {
                return NotFound($"Purchase with ID {purchaseDetail.PurchaseId} not found.");
            }

            if (product == null)
            {
                return NotFound($"Product with ID {purchaseDetail.ProductId} not found.");
            }

            // Set the navigation properties
            purchaseDetail.Purchase = purchase;
            purchaseDetail.Product = product;

            // Add the PurchaseDetail
            _context.PurchaseDetails.Add(purchaseDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPurchaseDetail), new { id = purchaseDetail.Id }, purchaseDetail);
        }


        // PUT: api/PurchaseDetail/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePurchaseDetail(int id, PurchaseDetail purchaseDetail)
        {
            if (id != purchaseDetail.Id)
                return BadRequest("PurchaseDetail ID mismatch.");

            try
            {
                _context.Entry(purchaseDetail).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
        private bool PurchaseDetailExists(int id)
        {
            return _context.PurchaseDetails.Any(pd => pd.Id == id);
        }

        // DELETE: api/PurchaseDetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseDetail(int id)
        {
            var purchaseDetail = await _context.Set<PurchaseDetail>().FindAsync(id);
            if (purchaseDetail == null)
                return NotFound();
            _context.Remove(purchaseDetail);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
