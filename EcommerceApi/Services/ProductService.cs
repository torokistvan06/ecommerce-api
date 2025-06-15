using EcommerceApi.Exceptions;
using EcommerceApi.Models;
using EcommerceApi.Repositories;

namespace EcommerceApi.Services;

public class ProductService(IProductRepository repository): IProductService
{
    private readonly IProductRepository _repository = repository;

    public async Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(product, cancellationToken).ConfigureAwait(false);
    }

    public async Task DeleteAsync(Product product, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(product, cancellationToken).ConfigureAwait(false);
    }

    public async Task<(IEnumerable<Product> products, int totalItems)> GetProductsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _repository.GetProductsAsync(pageNumber, pageSize, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _repository.GetProductByIdAsync(id, cancellationToken) ?? throw new NotFoundException("Product not found");
    }
}
