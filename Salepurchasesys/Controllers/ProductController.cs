using Microsoft.AspNetCore.Mvc;
using SalePurchasesys.DTOs;
using SalePurchasesys.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
        return Ok(await _productService.GetAllProductsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(ProductCreateDto productDto)
    {
        var createdProduct = await _productService.CreateProductAsync(productDto);
        return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDto productDto)
    {
        if (id != productDto.Id)
            return BadRequest("Product ID mismatch.");

        var updated = await _productService.UpdateProductAsync(id, productDto);
        if (updated == null) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteProductAsync(id);
        return NoContent();
    }
}
