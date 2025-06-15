using Bogus;
using Bogus.DataSets;
using EcommerceApi.Attributes;
using EcommerceApi.Models;
using EcommerceApi.Repositories;
using System;

namespace EcommerceApi.Data;

[DependsOn(typeof(CategorySeeder))]
public class ProductSeeder(IProductRepository productRepository, ICategoryRepository categoryRepository): ISeeder
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task SeedAsync(int count = 500, CancellationToken cancellationToken = default)
    {
        var (items, totalItems) = await _productRepository.GetProductsAsync(1, 1, cancellationToken);

        var categories = await _categoryRepository.GetAllCategoriesAsync(cancellationToken);
        
        if (categories == null || !categories.Any())
        {
            throw new InvalidOperationException("No categories found. Please seed categories first.");
        }

        if (items.Any())
        {
            return;
        }

        var productFaker = new Faker<Product>()
            .UseSeed(DateTime.UtcNow.Millisecond)
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.ShortDescription, f => f.Lorem.Sentence(3))
            .RuleFor(p => p.LongDescription, f => f.Lorem.Paragraphs(3, 5))
            .RuleFor(p => p.PrimaryImageUrl, f => f.Image.PicsumUrl())
            .RuleFor(p => p.CreatedAt, f => f.Date.Past(1).ToUniversalTime())
            .RuleFor(p => p.UpdatedAt, f => f.Date.Recent(1).ToUniversalTime())
            .RuleFor(p => p.PublishedAt, f => f.Date.Recent(1).ToUniversalTime())
            .RuleFor(p => p.Slug, f => f.Lorem.Slug())
            .RuleFor(p => p.MetaTitle, f => f.Lorem.Sentence(5))
            .RuleFor(p => p.MetaDescription, f => f.Lorem.Sentence(10))
            .RuleFor(p => p.MetaKeywords, f => string.Join(",", f.Lorem.Words(5)))
            .RuleFor(p => p.Price, f => (int)decimal.Parse(f.Commerce.Price()))
            .RuleFor(p => p.CategoryId, f => f.PickRandom(categories.Select(c => c.Id).ToList()));

        var fakeProducts = productFaker.Generate(count);
        foreach (var fakeProduct in fakeProducts)
        {
            await _productRepository.AddAsync(fakeProduct, cancellationToken);
        }
    }
}
