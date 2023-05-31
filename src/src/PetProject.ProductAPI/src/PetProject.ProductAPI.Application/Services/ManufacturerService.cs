using Microsoft.Extensions.Logging;
using AutoMapper;
using PetProject.ProductAPI.Domain.Manufacturer.Entitys;
using PetProject.ProductAPI.Domain.Interfaces.Repositories;
using PetProject.ProductAPI.Application.Contracts.Interfaces;
using PetProject.ProductAPI.Application.Contracts.Dto.Manufacturer;

namespace PetProject.ProductAPI.Application.Services;

public class ManufacturerService : IManufacturerService
{
    private readonly ILogger _logger;
    private readonly IManufacturerRepository _repository;
    private readonly IMapper _mapper;
    public ManufacturerService(
        ILogger<ManufacturerService> logger,
        IManufacturerRepository repository,
        IMapper mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<ManufacturerDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var manufactures = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<Manufacturer>, List<ManufacturerDto>>(manufactures);
    }

    public async Task<ManufacturerDto> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var manufacturer = await _repository.GetAsync(id);
        return _mapper.Map<Manufacturer, ManufacturerDto>(manufacturer);
    }

    public async Task<Guid> InsertOneAsync(CreateManufacturerDto input, CancellationToken cancellationToken = default)
    {
        var manufacturer = Manufacturer.Create(input.Name, input.Website);
        return await _repository.InsertOneAsync(manufacturer);
    }

    public async Task ReplaceOneAsync(ManufacturerDto input, CancellationToken cancellationToken = default)
    {
        var manufacturer = await _repository.GetAsync(input.Id);

        manufacturer
            .ChangeName(input.Name)
            .ChangeWebsite(input.Website);

        await _repository.ReplaceOneAsync(manufacturer);
    }
    public async Task DeleteOneAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _repository.DeleteOneAsync(id);
    }
}
