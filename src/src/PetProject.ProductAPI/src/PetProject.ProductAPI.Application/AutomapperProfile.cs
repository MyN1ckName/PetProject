﻿using AutoMapper;
using PetProject.ProductAPI.Domain.Product.Entity;
using PetProject.ProductAPI.Application.Contracts.Dto.Product;

namespace PetProject.ProductAPI.Application;

internal class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(x => x.Name, opt => opt.MapFrom(y => y.Name.Value))
            .ForMember(x => x.Category, opt => opt.MapFrom(y => y.Category.Value))
            .ForMember(x => x.Price, opt => opt.MapFrom(y => y.Price.Value));
    }
}
