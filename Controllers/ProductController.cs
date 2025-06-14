using AutoMapper;
using EcommerceApi.DTO.Outgoing;
using EcommerceApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers;

[ApiController]
[Route("api/v1/products")]
public class ProductController(IMapper mapper, ProductService productService) : Controller
{
    private readonly IMapper _mapper = mapper;
    private readonly ProductService _service = productService;

    [HttpGet]
    public async Task<ActionResult<PaginationDto<ProductTeaserDto>>> GetProducts(
        int pageNumber = 1,
        int pageSize = 10)
    {
        var (products, totalItems) = await _service.GetProductsAsync(pageNumber, pageSize);

        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var result = new PaginationDto<ProductTeaserDto>
        {
            Items = _mapper.Map<IEnumerable<ProductTeaserDto>>(products),
            TotalPages = totalPages,
            PageSize = pageSize,
            PageNumber = pageNumber,
        };

        return Ok(result);
    }

}
