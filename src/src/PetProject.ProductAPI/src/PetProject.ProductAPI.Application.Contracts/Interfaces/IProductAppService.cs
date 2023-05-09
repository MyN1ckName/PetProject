using PetProject.ProductAPI.Application.Contracts.Dto.Product;
using PetProject.ProductAPI.Domain.Product.Entity;

namespace PetProject.ProductAPI.Application.Contracts.Interfaces;

public interface IProductAppService
{
    Task<ProductDto> GetAsync(Guid id);
    Task<List<ProductDto>> GetAllAsync();
    Task InsertOneAsync(CreateProductDto input);
    Task UpdateOneAsync(ProductDto input);
}
