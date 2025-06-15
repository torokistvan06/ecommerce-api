using System;

namespace EcommerceApi.DTO.Outgoing;

public class ProductTeaserDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string ShortDescription { get; set; }
    public required string PrimaryImageUrl { get; set; }
    public required string Slug { get; set; }
    public required int Price { get; set; }
}
