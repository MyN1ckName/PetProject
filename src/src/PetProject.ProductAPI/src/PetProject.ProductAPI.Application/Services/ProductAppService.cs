using AutoMapper;
using Microsoft.Extensions.Logging;
using PetProject.ProductAPI.Domain.Product.Entity;
using PetProject.ProductAPI.Domain.Interfaces.Repositories;
using PetProject.ProductAPI.Application.Contracts.Interfaces;
using PetProject.ProductAPI.Application.Contracts.Dto.Product;

namespace PetProject.ProductAPI.Application.Services;

public class ProductAppService : IProductAppService
{
    private readonly ILogger _logger;
    private readonly IProductRepository<Guid> _productRepository;
    private readonly IMapper _mapper;
    public ProductAppService(
        ILogger<ProductAppService> logger,
        IProductRepository<Guid> productRepository,
        IMapper mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ProductDto> GetAsync(Guid id)
    {
        var product = await _productRepository.GetAsync(id);
        return _mapper.Map<Product, ProductDto>(product);
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return _mapper.Map<List<Product>, List<ProductDto>>(products);
    }

    public async Task InsertOneAsync(CreateProductDto input)
    {
        var product = new Product(input.Name, input.Category, input.Price);
        await _productRepository.InsertOneAsync(product);
    }

    public async Task UpdateOneAsync(ProductDto input)
    {
        var product = await _productRepository.GetAsync(input.Id);

        product
            .SetName(input.Name)
            .SetCategory(input.Category)
            .SetPrice(input.Price);

        await _productRepository.UpdateOneAsync(product);
    }

    public async Task DeleteOneAsync(Guid id)
    {
        await _productRepository.DeleteOneAsync(id);
    }
}
