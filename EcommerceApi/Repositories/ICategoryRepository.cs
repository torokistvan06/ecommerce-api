using EcommerceApi.Models;

namespace EcommerceApi.Repositories;

public interface ICategoryRepository
{
    Task AddAsync(Category category, CancellationToken cancellationToken);
    Task UpdateAsync(Category category, CancellationToken cancellationToken);
    Task DeleteAsync(Category category, CancellationToken cancellationToken);
    Task<Category?> GetCategoryByIdAsync(int id, CancellationToken cancellationToken);
    Task<(IEnumerable<Category> categories, int totalItems)> GetCategoriesAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<IEnumerable<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken);
    Task<bool> ExistsAsync(int categoryId, CancellationToken cancellationToken);

}
