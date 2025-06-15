namespace EcommerceApi.DTO.Incoming;

public class ProductCreationDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int Price { get; set; }
}
