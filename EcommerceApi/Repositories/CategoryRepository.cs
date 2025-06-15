using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace EcommerceApi.Repositories;

public class CategoryRepository(AppDbContext dbContext): ICategoryRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly DbSet<Category> _dbSet = dbContext.Categories;
    public async Task<(IEnumerable<Category> categories, int totalItems)> GetCategoriesAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var totalItems = await _dbSet.CountAsync(cancellationToken);
        var items = await _dbSet
            .OrderBy(c => c.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        return (items, totalItems);
    }
    public async Task AddAsync(Category category, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(category, cancellationToken).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
    public async Task DeleteAsync(Category category, CancellationToken cancellationToken)
    {
        _dbSet.Remove(category);
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
    public async Task<Category?> GetCategoryByIdAsync(int id, CancellationToken cancellationToken) =>
        await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken)
            .ConfigureAwait(false);
    public async Task UpdateAsync(Category category, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken) =>
        await _dbSet
            .AsNoTracking()
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    public async Task<bool> ExistsAsync(int categoryId, CancellationToken cancellationToken)
    {
        return await _dbSet.AnyAsync(c => c.Id == categoryId, cancellationToken);
    }
}
