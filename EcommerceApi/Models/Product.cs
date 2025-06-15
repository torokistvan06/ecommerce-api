namespace EcommerceApi.Models;

public class Product
{
    public required int Id { get; set; }
    // Descriptive content
    public required string Name { get; set; }
    public required string ShortDescription { get; set; }
    public required string LongDescription { get; set; }
    public required string PrimaryImageUrl { get; set; }
    // Timestamps
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public required DateTime PublishedAt { get; set; }
    // SEO
    public required string Slug { get; set; }
    public required string MetaTitle { get; set; }
    public required string MetaDescription { get; set; }
    public required string MetaKeywords { get; set; }
    // Price
    public required int Price { get; set; }
    // Relationships
    public required int CategoryId { get; set; }
    public required Category Category { get; set; }

}