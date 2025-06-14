using Microsoft.EntityFrameworkCore;
using EcommerceApi.Repository;
using EcommerceApi.Service;
using Scalar.AspNetCore;
using EcommerceApi;
using EcommerceApi.Data;

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
        builder.Services.AddScoped<ProductRepository>();
        builder.Services.AddScoped<ProductService>();
        builder.Services.AddScoped<MinioService>();
        builder.Services.AddScoped<ProductSeeder>();


        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();

            using (var scope = app.Services.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<ProductSeeder>();
                await seeder.SeedAsync();
            }

        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseMiddleware<ExceptionHandler>();

        app.MapControllers();

        app.Run();
    }
}