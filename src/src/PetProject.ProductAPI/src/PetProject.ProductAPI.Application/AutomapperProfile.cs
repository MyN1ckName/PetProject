﻿using AutoMapper;
using PetProject.ProductAPI.Domain.Product.Entitys;
using PetProject.ProductAPI.Domain.Manufacturer.Entitys;
using PetProject.ProductAPI.Application.Contracts.Dto.Product;
using PetProject.ProductAPI.Application.Contracts.Dto.Manufacturer;

namespace PetProject.ProductAPI.Application;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(x => x.Name, opt => opt.MapFrom(y => y.Name.Value))
            .ForMember(x => x.Category, opt => opt.MapFrom(y => y.Category.Value))
            .ForMember(x => x.Price, opt => opt.MapFrom(y => Math.Round(y.Price.Value, 2)));

        CreateMap<Manufacturer, ManufacturerDto>()
            .ForMember(x => x.Name, opt => opt.MapFrom(y => y.Name.Value));
    }
}
