﻿using PetProject.ProductAPI.Application.Contracts.Dto.Product;

namespace PetProject.ProductAPI.Application.Contracts.Interfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetAllAsync();
}
