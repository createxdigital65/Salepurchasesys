using Microsoft.AspNetCore.Mvc;
using SalePurchasesys.DTOs;
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
        public async Task<ActionResult<IEnumerable<PurchaseDto>>> GetPurchases()
        {
            var purchases = await _purchaseService.GetAllPurchasesAsync();
            return Ok(purchases);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseDto>> GetPurchase(int id)
        {
            var purchase = await _purchaseService.GetPurchaseByIdAsync(id);
            if (purchase == null) return NotFound();
            return Ok(purchase);
        }

        [HttpPost]
        public async Task<ActionResult<PurchaseDto>> CreatePurchase(CreatePurchaseDto dto)
        {
            var created = await _purchaseService.CreatePurchaseAsync(dto);
            return CreatedAtAction(nameof(GetPurchase), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePurchase(int id, UpdatePurchaseDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var updated = await _purchaseService.UpdatePurchaseAsync(id, dto);
            if (updated == null) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(int id)
        {
            var success = await _purchaseService.DeletePurchaseAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
