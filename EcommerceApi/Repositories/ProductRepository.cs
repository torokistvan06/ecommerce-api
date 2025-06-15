using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repositories;

public class ProductRepository(AppDbContext dbContext): IProductRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly DbSet<Product> _dbSet = dbContext.Products;

    public async Task<(IEnumerable<Product> products, int totalItems)> GetProductsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var totalItems = await _dbSet.CountAsync(cancellationToken);

        var items = await _dbSet
            .OrderBy(p => p.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalItems);
    }

    public async Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(product, cancellationToken).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task DeleteAsync(Product product, CancellationToken cancellationToken)
    {
        _dbSet.Remove(product);
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken) =>
        await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
            .ConfigureAwait(false);

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<(IEnumerable<Product> products, int totalItems)> GetProductsByCategoryAsync(int categoryId, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var query = _dbSet.Where(p => p.CategoryId == categoryId);

        var totalItems = await query.CountAsync(cancellationToken);
        var items = await query
            .OrderBy(p => p.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalItems);
    }
}
