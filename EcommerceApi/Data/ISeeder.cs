namespace EcommerceApi.Data;

public interface ISeeder
{
    public Task SeedAsync(int count = 50, CancellationToken cancellationToken = default);
}
