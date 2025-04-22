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
    public class PurchaseDetailController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PurchaseDetailController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseDetailDto>>> GetPurchaseDetails()
        {
            var details = await _context.PurchaseDetails
                .Include(pd => pd.Purchase)
                .Include(pd => pd.Product)
                .ToListAsync();

            return Ok(_mapper.Map<List<PurchaseDetailDto>>(details));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseDetailDto>> GetPurchaseDetail(int id)
        {
            var detail = await _context.PurchaseDetails
                .Include(pd => pd.Purchase)
                .Include(pd => pd.Product)
                .FirstOrDefaultAsync(pd => pd.Id == id);

            if (detail == null) return NotFound();

            return Ok(_mapper.Map<PurchaseDetailDto>(detail));
        }

        [HttpPost]
        public async Task<ActionResult<PurchaseDetailDto>> CreatePurchaseDetail(CreatePurchaseDetailDto dto)
        {
            var purchase = await _context.Purchases.FindAsync(dto.PurchaseId);
            var product = await _context.Products.FindAsync(dto.ProductId);

            if (purchase == null)
                return NotFound($"Purchase with ID {dto.PurchaseId} not found.");
            if (product == null)
                return NotFound($"Product with ID {dto.ProductId} not found.");

            var detail = _mapper.Map<PurchaseDetail>(dto);
            _context.PurchaseDetails.Add(detail);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<PurchaseDetailDto>(detail);
            return CreatedAtAction(nameof(GetPurchaseDetail), new { id = resultDto.Id }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePurchaseDetail(int id, UpdatePurchaseDetailDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var detail = await _context.PurchaseDetails.FindAsync(id);
            if (detail == null) return NotFound();

            _mapper.Map(dto, detail);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseDetail(int id)
        {
            var detail = await _context.PurchaseDetails.FindAsync(id);
            if (detail == null) return NotFound();

            _context.PurchaseDetails.Remove(detail);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
