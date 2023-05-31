using PetProject.ProductAPI.Application.Contracts.Dto.Manufacturer;

namespace PetProject.ProductAPI.Application.Contracts.Interfaces;

public interface IManufacturerService
{
    Task<ManufacturerDto> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<ManufacturerDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Guid> InsertOneAsync(CreateManufacturerDto input, CancellationToken cancellationToken = default);
    Task ReplaceOneAsync(ManufacturerDto input, CancellationToken cancellationToken = default);
    Task DeleteOneAsync(Guid id, CancellationToken cancellationToken = default);
}
