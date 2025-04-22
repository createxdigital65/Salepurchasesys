using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalePurchasesys.DTOs;
using SalePurchasesys.Models;
using SalePurchasesys.Services;

[ApiController]
[Route("api/[controller]")]
public class SaleController : ControllerBase
{
    private readonly ISaleService _saleService;
    private readonly IMapper _mapper;

    public SaleController(ISaleService saleService, IMapper mapper)
    {
        _saleService = saleService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SaleDto>>> GetSales()
    {
        var sales = await _saleService.GetAllSalesAsync();
        var salesDto = _mapper.Map<IEnumerable<SaleDto>>(sales);
        return Ok(salesDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SaleDto>> GetSale(int id)
    {
        var sale = await _saleService.GetSaleByIdAsync(id);
        if (sale == null)
            return NotFound();

        var saleDto = _mapper.Map<SaleDto>(sale);
        return Ok(saleDto);
    }

    [HttpPost]
    public async Task<ActionResult<SaleDto>> CreateSale([FromBody] SaleCreateDto saleCreateDto)
    {
        if (saleCreateDto.SaleDetails == null || !saleCreateDto.SaleDetails.Any())
        {
            return BadRequest("At least one SaleDetail is required.");
        }

        var sale = _mapper.Map<Sale>(saleCreateDto);

        foreach (var detail in sale.SaleDetails)
        {
            var product = await _saleService.GetProductByIdAsync(detail.ProductId);
            if (product == null)
            {
                return NotFound($"Product with ID {detail.ProductId} not found.");
            }

            detail.CalculateSubtotal(product.Price);
        }

        var createdSale = await _saleService.CreateSaleAsync(sale);
        var createdSaleDto = _mapper.Map<SaleDto>(createdSale);
        return CreatedAtAction(nameof(GetSale), new { id = createdSaleDto.Id }, createdSaleDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSale(int id, [FromBody] SaleUpdateDto saleUpdateDto)
    {
        if (id != saleUpdateDto.Id)
            return BadRequest();

        var sale = _mapper.Map<Sale>(saleUpdateDto);

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
