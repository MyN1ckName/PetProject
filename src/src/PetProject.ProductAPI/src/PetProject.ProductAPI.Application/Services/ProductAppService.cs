using AutoMapper;
using Microsoft.Extensions.Logging;
using PetProject.ProductAPI.Domain.Product.Entitys;
using PetProject.ProductAPI.Domain.Interfaces.Repositories;
using PetProject.ProductAPI.Application.Contracts.Interfaces;
using PetProject.ProductAPI.Application.Contracts.Dto.Product;

namespace PetProject.ProductAPI.Application.Services;

public class ProductAppService : IProductAppService
{
    private readonly ILogger _logger;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    public ProductAppService(
        ILogger<ProductAppService> logger,
        IProductRepository productRepository,
        IMapper mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ProductDto> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetAsync(id);
        return _mapper.Map<Product, ProductDto>(product);
    }

    public async Task<List<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.GetAllAsync();
        return _mapper.Map<List<Product>, List<ProductDto>>(products);
    }

    public async Task<Guid> InsertOneAsync(CreateProductDto input, CancellationToken cancellationToken = default)
    {
        var product = new Product(input.Name, input.Category, input.Price);
        return await _productRepository.InsertOneAsync(product);
    }

    public async Task UpdateOneAsync(ProductDto input, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetAsync(input.Id);

        product
            .SetName(input.Name)
            .SetCategory(input.Category)
            .SetPrice(input.Price);

        await _productRepository.UpdateOneAsync(product);
    }

    public async Task DeleteOneAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _productRepository.DeleteOneAsync(id);
    }
}
