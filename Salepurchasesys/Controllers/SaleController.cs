using Microsoft.AspNetCore.Mvc;
using SalePurchasesys.Models;
using SalePurchasesys.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalePurchasesys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
        {
            return Ok(await _saleService.GetAllSalesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetSale(int id)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);
            if (sale == null)
                return NotFound();
            return Ok(sale);
        }

        [HttpPost]
        public async Task<ActionResult<Sale>> CreateSale([FromBody] Sale sale)
        {
            if (sale.SaleDetails == null || sale.SaleDetails.Count == 0)
            {
                return BadRequest("At least one SaleDetail is required.");
            }

            // Optionally, calculate subtotal for each sale detail (if needed)
            foreach (var detail in sale.SaleDetails)
            {
                var product = await _saleService.GetProductByIdAsync(detail.ProductId); // Use _saleService to fetch the product
                if (product == null)
                {
                    return NotFound($"Product with ID {detail.ProductId} not found.");
                }

                // Set Subtotal if not already set
                detail.CalculateSubtotal(product.Price);

                // Set SaleId for each SaleDetail (this is missing in your original data)
                detail.SaleId = sale.Id;
            }

            var createdSale = await _saleService.CreateSaleAsync(sale);
            return CreatedAtAction(nameof(GetSale), new { id = createdSale.Id }, createdSale);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale(int id, [FromBody] Sale sale)
        {
            if (id != sale.Id)
                return BadRequest();

            var updatedSale = await _saleService.UpdateSaleAsync(id, sale);
            if (updatedSale == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            var success = await _saleService.DeleteSaleAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
