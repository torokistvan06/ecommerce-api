using AutoMapper;
using EcommerceApi.DTO.Outgoing;
using EcommerceApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers;


[ApiController]
[Route("api/v1/categories")]
public class CategoryController(IMapper mapper, ICategoryService categoryService) : Controller
{
    private readonly IMapper _mapper = mapper;
    private readonly ICategoryService _service = categoryService;

    [HttpGet("{categoryId}/products")]
    public async Task<ActionResult<PaginationDto<ProductTeaserDto>>> GetProductsByCategory(
        [FromRoute] int categoryId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default
    )
    {
        var (products, totalItems) = await _service.GetProductsByCategoryIdAsync(
            categoryId, pageNumber, pageSize, cancellationToken);

        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var result = new PaginationDto<ProductTeaserDto>
        {
            Items = _mapper.Map<IEnumerable<ProductTeaserDto>>(products),
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = totalPages
        };

        return Ok(result);
    }

}
