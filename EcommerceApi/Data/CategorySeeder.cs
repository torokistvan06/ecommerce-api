using Bogus;
using EcommerceApi.Models;
using EcommerceApi.Repositories;

namespace EcommerceApi.Data;

class CategorySeeder(ICategoryRepository categoryRepository): ISeeder
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    public async Task SeedAsync(int count = 50, CancellationToken cancellationToken = default)
    {
        var (items, totalItems) = await _categoryRepository.GetCategoriesAsync(1, 1, cancellationToken);
        if (!items.Any())
        {
            var categoryFaker = new Faker<Category>()
                .UseSeed(DateTime.UtcNow.Millisecond)
                .RuleFor(c => c.Name, f => f.Commerce.Categories(1).FirstOrDefault())
                .RuleFor(c => c.Description, f => f.Lorem.Sentence(5))
                .RuleFor(c => c.CreatedAt, f => f.Date.Past(1).ToUniversalTime())
                .RuleFor(c => c.UpdatedAt, f => f.Date.Recent(1).ToUniversalTime())
                .RuleFor(c => c.PublishedAt, f => f.Date.Recent(1).ToUniversalTime())
                .RuleFor(c => c.Slug, f => f.Lorem.Slug())
                .RuleFor(c => c.MetaTitle, f => f.Lorem.Sentence(5))
                .RuleFor(c => c.MetaDescription, f => f.Lorem.Sentences(5))
                .RuleFor(c => c.MetaKeywords, f => string.Join(",", f.Lorem.Words(5)));
            var fakeCategories = categoryFaker.Generate(count);
            foreach (var fakeCategory in fakeCategories)
            {
                await _categoryRepository.AddAsync(fakeCategory, cancellationToken);
            }
        }
    }
}
