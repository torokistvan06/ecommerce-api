using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EcommerceApi.Models;

namespace EcommerceApi.Repository;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public AppDbContext(IConfiguration configuration) 
    {
        _configuration = configuration;
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseNpgsql(connectionString);
    }
}
