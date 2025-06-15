using EcommerceApi.Exceptions;
using EcommerceApi.Models;
using EcommerceApi.Repositories;

namespace EcommerceApi.Services;

public class CategoryService(ICategoryRepository categoryRepository, IProductRepository productRepository) : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IProductRepository _productRepository = productRepository;

    public Task AddAsync(Category category, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Category category, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<(IEnumerable<Category> categories, int totalItems)> GetCategoriesAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Category?> GetCategoryByIdAsync(int id, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(id, cancellationToken);
        return category is null ? throw new NotFoundException("Category not found") : category;
    }

    public async Task<(IEnumerable<Product>, int totalItems)> GetProductsByCategoryIdAsync(int id, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var exists = await _categoryRepository.ExistsAsync(id, cancellationToken);

        if (!exists)
        {
            throw new NotFoundException("Category not found");
        }

        return await _productRepository.GetProductsByCategoryAsync(id, pageNumber, pageSize, cancellationToken);
    }

    public Task UpdateAsync(Category category, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
