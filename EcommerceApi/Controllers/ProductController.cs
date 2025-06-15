using AutoMapper;
using EcommerceApi.DTO.Outgoing;
using EcommerceApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers;

[ApiController]
[Route("api/v1/products")]
public class ProductController(IMapper mapper, IProductService productService) : Controller
{
    private readonly IMapper _mapper = mapper;
    private readonly IProductService _service = productService;

    [HttpGet]
    public async Task<ActionResult<PaginationDto<ProductTeaserDto>>> GetProducts(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default
    ) {
        if (pageNumber < 1)
            return BadRequest("Page number must be greater than 0");

        if (pageSize < 1 || pageSize > 100)
            return BadRequest("Page size must be between 1 and 100");


        var (products, totalItems) = await _service.GetProductsAsync(pageNumber, pageSize, cancellationToken);

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
