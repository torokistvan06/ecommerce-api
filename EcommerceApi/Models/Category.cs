namespace EcommerceApi.Models;

public class Category
{
    public required int Id { get; set; }
    // Descriptive content
    public required string Name { get; set; }
    public required string Description { get; set; }
    // Timestamps
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public required DateTime PublishedAt { get; set; }
    // SEO
    public required string Slug { get; set; }
    public required string MetaTitle { get; set; }
    public required string MetaDescription { get; set; }
    public required string MetaKeywords { get; set; }
    // Relationships
    public required Category? ParentCategory { get; set; }
    public List<Category> ChildCategories { get; set; } = [];
    public List<Product> Products { get; set; } = [];
}
