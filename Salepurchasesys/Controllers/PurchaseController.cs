using Microsoft.AspNetCore.Mvc;
using SalePurchasesys.Models;
using SalePurchasesys.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalePurchasesys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Purchase>>> GetPurchases()
        {
            return Ok(await _purchaseService.GetAllPurchasesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Purchase>> GetPurchase(int id)
        {
            var purchase = await _purchaseService.GetPurchaseByIdAsync(id);
            if (purchase == null)
                return NotFound();
            return Ok(purchase);
        }

        [HttpPost]
        public async Task<ActionResult<Purchase>> CreatePurchase(Purchase purchase)
        {
            var createdPurchase = await _purchaseService.CreatePurchaseAsync(purchase);
            return CreatedAtAction(nameof(GetPurchase), new { id = createdPurchase.Id }, createdPurchase);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePurchase(int id, Purchase purchase)
        {
            if (id != purchase.Id)
                return BadRequest();

            var updatedPurchase = await _purchaseService.UpdatePurchaseAsync(id, purchase);
            if (updatedPurchase == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(int id)
        {
            var success = await _purchaseService.DeletePurchaseAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
