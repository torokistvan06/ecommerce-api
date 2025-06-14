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
    public async Task<(List<Product> items, int totalItems)> GetProductsAsync(int pageNumber, int pageSize)
    {
        var totalItems = await _dbSet.CountAsync();

        var items = await _dbSet
            .OrderBy(p => p.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalItems);
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

    public async Task<Product> UpdateProductAsync(Product product)
    {
        _dbSet.Entry(product).CurrentValues.SetValues(product);
        await _context.SaveChangesAsync().ConfigureAwait(false); 
        return product;  
    }
}
