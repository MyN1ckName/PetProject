﻿using PetProject.ProductAPI.Application.Contracts.Dto.Product;

namespace PetProject.ProductAPI.Application.Contracts.Interfaces;

public interface IProductAppService
{
    Task<ProductDto> GetAsync(Guid id);
    Task<List<ProductDto>> GetAllAsync();
    Task InsertOneAsync(CreateProductDto input);
    Task UpdateOneAsync(ProductDto input);
    Task DeleteOneAsync(Guid id);
}
