using AutoMapper;
using EcommerceApi.DTO.Incoming;
using EcommerceApi.DTO.Outgoing;
using EcommerceApi.Models;
using EcommerceApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers;

[Route("api/v1/products")]
[ApiController]
public class ProductController(ProductService service, IMapper mapper) : ControllerBase
{
    private readonly ProductService _service = service;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult<List<ProductTeaserDto>>>GetProducts() 
    {
        var products = await _service.GetProductsAsync().ConfigureAwait(false);
        var result = _mapper.Map<ProductTeaserDto[]>(products);
        return Ok(result);
    }   

    [HttpPost]
    public async Task<ActionResult<ProductTeaserDto>> CreateProduct(ProductCreationDto dto) 
    {
        var product = _mapper.Map<Product>(dto);
        var createdProduct = await _service.CreateProductAsync(product).ConfigureAwait(false);
        var result = _mapper.Map<ProductTeaserDto>(createdProduct);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetProductById(int id)
    {
        var product = await _service.GetProductById(id);
        var result = _mapper.Map<ProductTeaserDto>(product);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProductById(int id)
    {
        return Ok();
    }

    [HttpPost("{id}/image")]
    public async Task<ActionResult> AddImageToProduct(int id)
    {
        return Ok();
    }
}
