using Bogus;
using EcommerceApi.Models;
using EcommerceApi.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Xunit;

namespace EcommerceApi.Tests.Repositories;

public class CategoryRepositoryTests : IDisposable
{
    private readonly AppDbContext _context;
    private readonly CategoryRepository _repository;
    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    public CategoryRepositoryTests()
    {
        // Set up in-memory database for testing
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
        _repository = new CategoryRepository(_context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    private List<Category>SeedCategories(int count)
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
        return fakeCategories;
    }

    #region GetCategoriesAsync Tests

    [Fact]
    public async Task GetCategoriesAsync_WithEmptyDatabase_ReturnsEmptyListAndZeroCount()
    {
        // Act
        var (categories, totalItems) = await _repository.GetCategoriesAsync(1, 10, _cancellationToken);

        // Assert
        Assert.Empty(categories);
        Assert.Equal(0, totalItems);
    }


    [Fact]
    public async Task GetCategoriesAsync_WithData_ReturnsCorrectCategoriesAndCount()
    {
        // Arrange
        var expectedCategories = SeedCategories(3);
        foreach (var category in expectedCategories)
        {
            await _context.AddAsync(category, _cancellationToken);
        }
        await _context.SaveChangesAsync();

        // Act
        var (categories, totalItems) = await _repository.GetCategoriesAsync(1, 10, _cancellationToken);

        // Assert
        Assert.Equal(3, totalItems);
        Assert.Equal(3, categories.Count());
        Assert.All(categories, c => Assert.Contains(expectedCategories, ec => ec.Id == c.Id));
    }

    #endregion
}