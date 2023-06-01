using AutoMapper;
using Microsoft.Extensions.Logging;
using PetProject.ProductAPI.Domain.Products;
using PetProject.ProductAPI.Domain.Products.Entitys;
using PetProject.ProductAPI.Domain.Interfaces.Repositories;
using PetProject.ProductAPI.Application.Contracts.Interfaces;
using PetProject.ProductAPI.Application.Contracts.Dto.Product;

namespace PetProject.ProductAPI.Application.Services;

public class ProductAppService : IProductAppService
{
    private readonly ILogger _logger;
    private readonly IProductRepository _productRepository;
    private readonly ProductManager _productManager;
    private readonly IMapper _mapper;
    public ProductAppService(
        ILogger<ProductAppService> logger,
        IProductRepository productRepository,
        ProductManager productManager,
        IMapper mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _productManager = productManager ?? throw new ArgumentNullException(nameof(productManager));
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
        var product = await _productManager.CreateProductAsync(input.Name, input.Category, input.Price, input.ManufacturerId);
        return await _productRepository.InsertOneAsync(product);
    }

    public async Task ReplaceOneAsync(ProductDto input, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetAsync(input.Id);

        product
            .SetName(input.Name)
            .SetCategory(input.Category)
            .SetPrice(input.Price);

        await _productRepository.ReplaceOneAsync(product);
    }

    public async Task DeleteOneAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _productRepository.DeleteOneAsync(id);
    }
}
