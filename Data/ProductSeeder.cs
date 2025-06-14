using System;
using Bogus;
using EcommerceApi.Models;
using EcommerceApi.Repository;

namespace EcommerceApi.Data;

public class ProductSeeder(ProductRepository productRepository)
{
    private readonly ProductRepository _productRepository = productRepository;

    public async Task SeedAsync()
    {
        var (items, totalItems) = await _productRepository.GetProductsAsync(1, 1);

        if (items.Count == 0)
        {
            var productFaker = new Faker<Product>()
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Price, f => (int) decimal.Parse(f.Commerce.Price()))
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription());

            var fakeProducts = productFaker.Generate(500);
            foreach (var fakeProduct in fakeProducts)
            {
                await _productRepository.CreateProductAsync(fakeProduct);
            }
        }
    }
}
