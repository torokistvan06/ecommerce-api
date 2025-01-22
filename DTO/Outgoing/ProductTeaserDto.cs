using System;

namespace EcommerceApi.DTO.Outgoing;

public class ProductTeaserDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Price { get; set; }
    public string? ImagePath { get; set; }
}
