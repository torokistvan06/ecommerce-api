using AutoMapper;
using EcommerceApi.DTO.Incoming;
using EcommerceApi.DTO.Outgoing;
using EcommerceApi.Models;

namespace EcommerceApi;

public class AppMapper: Profile
{
    public AppMapper()
    {
        CreateMap<ProductCreationDto, Product>();
        CreateMap<Product, ProductTeaserDto>();
    }
}
