using EcommerceApi.Models;

namespace EcommerceApi.Repositories;

public interface IProductRepository
{
    Task AddAsync(Product product, CancellationToken cancellationToken);
    Task UpdateAsync(Product product, CancellationToken cancellationToken);
    Task DeleteAsync(Product product, CancellationToken cancellationToken);
    Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken);
    Task<(IEnumerable<Product> products, int totalItems)> GetProductsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<(IEnumerable<Product> products, int totalItems)> GetProductsByCategoryAsync(int categoryId, int pageNumber, int pageSize, CancellationToken cancellationToken);
}
