using Application.Contracts.Requests;
using Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
    {
        await _service.CreateProduct(request);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductRequest request)
    {
        await _service.UpdateProduct(id, request);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
    {
        await _service.DeleteProduct(id);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById([FromRoute] Guid id)
    {
        var product = await _service.GetProductById(id);
        return Ok(product);
    }

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetProductByName([FromRoute] string name)
    {
        var product = await _service.GetProductByName(name);
        return Ok(product);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _service.GetAllProducts();
        return Ok(products);
    }
}