using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EcommerceApi.Models;

namespace EcommerceApi.Repositories;

public class AppDbContext : DbContext
{
    private readonly IConfiguration? _configuration;

    public AppDbContext(IConfiguration configuration) 
    {
        _configuration = configuration;
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        _configuration = null; // No configuration needed for testing
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Only configure if not already configured AND we have configuration
        if (!optionsBuilder.IsConfigured && _configuration != null)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);
        }
        // If _configuration is null (testing scenario), do nothing - options are already configured
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasOne(c => c.ParentCategory)
            .WithMany(c => c.ChildCategories)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
}
