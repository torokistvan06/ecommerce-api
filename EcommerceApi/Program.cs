using Microsoft.EntityFrameworkCore;
using EcommerceApi.Services;
using Scalar.AspNetCore;
using EcommerceApi;
using EcommerceApi.Data;
using EcommerceApi.Repositories;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().AddNewtonsoftJson();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddAutoMapper(typeof(AppMapper));

        // Repositories
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

        // Services
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        
        // Data Seeders
        builder.Services.AddScoped<ISeeder, ProductSeeder>();
        builder.Services.AddScoped<ISeeder, CategorySeeder>();
        builder.Services.AddScoped<DataSeederRunner>();

        // Minio
        builder.Services.AddScoped<MinioService>(); 


        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();

            using var scope = app.Services.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<DataSeederRunner>();
            await runner.RunAsync();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseMiddleware<ExceptionHandler>();

        app.MapControllers();

        app.Run();
    }
}