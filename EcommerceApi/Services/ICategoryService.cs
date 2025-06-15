using EcommerceApi.Models;

namespace EcommerceApi.Services;

public interface ICategoryService
{
    Task AddAsync(Category category, CancellationToken cancellationToken);
    Task UpdateAsync(Category category, CancellationToken cancellationToken);
    Task DeleteAsync(Category category, CancellationToken cancellationToken);
    Task<Category?> GetCategoryByIdAsync(int id, CancellationToken cancellationToken);
    Task<(IEnumerable<Category> categories, int totalItems)> GetCategoriesAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<(IEnumerable<Product>, int totalItems)> GetProductsByCategoryIdAsync(int id, int pageNumber, int pageSize, CancellationToken cancellationToken);

}
