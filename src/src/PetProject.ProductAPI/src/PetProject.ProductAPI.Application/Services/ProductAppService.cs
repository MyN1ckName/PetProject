using AutoMapper;
using Microsoft.Extensions.Logging;
using PetProject.ProductAPI.Application.Contracts.Dto.Product;
using PetProject.ProductAPI.Application.Contracts.Interfaces;
using PetProject.ProductAPI.Domain.Product.Entity;
using PetProject.ProductAPI.Domain.Interfaces.Repositories;

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

    public async Task<List<ProductDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return _mapper.Map<List<Product>, List<ProductDto>>(products);
    }
}
