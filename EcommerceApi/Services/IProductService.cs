using EcommerceApi.Models;

namespace EcommerceApi.Services;

public interface IProductService
{
    Task AddAsync(Product product, CancellationToken cancellationToken);
    Task DeleteAsync(Product product, CancellationToken cancellationToken);
    Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken);
    Task<(IEnumerable<Product> products, int totalItems)> GetProductsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
}
