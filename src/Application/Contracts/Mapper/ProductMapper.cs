using Application.Contracts.Requests;
using Application.Contracts.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Contracts.Mapper;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<CreateProductRequest, Product>();
        CreateMap<UpdateProductRequest, Product>();
        CreateMap<Product, ProductResponse>();
    }
}