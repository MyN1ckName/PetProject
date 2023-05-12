using PetProject.ProductAPI.Application.Contracts.Dto.Product;

namespace PetProject.ProductAPI.Application.Contracts.Interfaces;

public interface IProductAppService
{
    Task<ProductDto> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Guid> InsertOneAsync(CreateProductDto input, CancellationToken cancellationToken = default);
    Task UpdateOneAsync(ProductDto input, CancellationToken cancellationToken = default);
    Task DeleteOneAsync(Guid id, CancellationToken cancellationToken = default);
}
