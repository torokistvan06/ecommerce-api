using EcommerceApi.Exceptions;
using EcommerceApi.Models;
using EcommerceApi.Repository;

namespace EcommerceApi.Service;

public class ProductService(ProductRepository repository, MinioService minioService)
{
    private readonly ProductRepository _repository = repository;
    private readonly MinioService _minioService = minioService;

    public async Task<(List<Product> items, int totalItems)> GetProductsAsync(int pageNumber, int pageSize)
    {
        return await _repository.GetProductsAsync(pageNumber, pageSize).ConfigureAwait(false);
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        return await _repository.CreateProductAsync(product).ConfigureAwait(false);
    }

    public async Task<Product> GetProductById(int id)
    {
        return await _repository.GetProductById(id) ?? throw new NotFoundException("Product not found");
    }

    public async Task<Product> AddImageToProduct(int id, IFormFile file)
    {
        var product = await _repository.GetProductById(id) ?? throw new NotFoundException("Product not found");

        var filePath = await _minioService.UploadImage(file);

        product.ImagePath = filePath;

        product = await _repository.UpdateProductAsync(product);

        return product;
    }

    public async Task<Product> RemoveImageFromProdcut(int id)
    {
        
        var product = await _repository.GetProductById(id) ?? throw new NotFoundException("Product not found");

        await _minioService.DeleteImage(product.ImagePath!);

        product.ImagePath = null;

        product = await _repository.UpdateProductAsync(product);

        return product;
    }
}
