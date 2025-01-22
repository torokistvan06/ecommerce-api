using System;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repository;

public class ProductRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Product> _dbSet;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Product>();
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await _dbSet.ToListAsync().ConfigureAwait(false);
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        await _dbSet.AddAsync(product).ConfigureAwait(false);
        await _context.SaveChangesAsync().ConfigureAwait(false);
        return product;
    }

    public async Task<Product?> GetProductById(int id)
    {
        return await _dbSet.FindAsync(id);
    }
}
