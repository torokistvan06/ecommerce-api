using System;
using EcommerceApi.Exceptions;
using EcommerceApi.Models;
using EcommerceApi.Repository;

namespace EcommerceApi.Service;

public class ProductService
{
    private readonly ProductRepository _repository;

    public ProductService(ProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await _repository.GetProductsAsync().ConfigureAwait(false);
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        return await _repository.CreateProductAsync(product).ConfigureAwait(false);
    }

    public async Task<Product> GetProductById(int id)
    {
        return await _repository.GetProductById(id) ?? throw new NotFoundException("Product not found");
    }
}
