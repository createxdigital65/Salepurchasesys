using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalePurchasesys.Data;
using SalePurchasesys.DTOs;
using SalePurchasesys.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalePurchasesys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleDetailController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SaleDetailController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDetailDto>>> GetSaleDetails()
        {
            var saleDetails = await _context.SaleDetails
                .Include(sd => sd.Sale)
                .Include(sd => sd.Product)
                .ToListAsync();

            return Ok(_mapper.Map<List<SaleDetailDto>>(saleDetails));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDetailDto>> GetSaleDetail(int id)
        {
            var saleDetail = await _context.SaleDetails
                .Include(sd => sd.Sale)
                .Include(sd => sd.Product)
                .FirstOrDefaultAsync(sd => sd.Id == id);

            if (saleDetail == null) return NotFound();

            return Ok(_mapper.Map<SaleDetailDto>(saleDetail));
        }

        [HttpPost]
        public async Task<ActionResult<SaleDetailDto>> CreateSaleDetail([FromBody] CreateSaleDetailDto dto)
        {
            var saleExists = await _context.Sales.AnyAsync(s => s.Id == dto.SaleId);
            var product = await _context.Products.FindAsync(dto.ProductId);

            if (!saleExists) return NotFound($"Sale with ID {dto.SaleId} not found.");
            if (product == null) return NotFound($"Product with ID {dto.ProductId} not found.");

            var saleDetail = _mapper.Map<SaleDetail>(dto);
            saleDetail.CalculateSubtotal(product.Price);

            _context.SaleDetails.Add(saleDetail);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<SaleDetailDto>(saleDetail);

            return CreatedAtAction(nameof(GetSaleDetail), new { id = saleDetail.Id }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSaleDetail(int id, [FromBody] UpdateSaleDetailDto dto)
        {
            if (id != dto.Id) return BadRequest("ID mismatch.");

            var saleDetail = await _context.SaleDetails.FindAsync(id);
            if (saleDetail == null) return NotFound();

            _mapper.Map(dto, saleDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaleDetail(int id)
        {
            var saleDetail = await _context.SaleDetails.FindAsync(id);
            if (saleDetail == null) return NotFound();

            _context.SaleDetails.Remove(saleDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
