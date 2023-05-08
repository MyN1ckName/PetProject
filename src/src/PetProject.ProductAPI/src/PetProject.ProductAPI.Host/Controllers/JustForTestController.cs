using Microsoft.AspNetCore.Mvc;
using PetProject.ProductAPI.Domain.Interfaces.Repositories;
using PetProject.ProductAPI.Domain.Product.Entity;
using PetProject.ProductAPI.Domain.Product.ValueObject;

namespace PetProject.ProductAPI.Host.Controllers;

[Route("/api/[controller]")]
public class JustForTestController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IProductRepository _productRepository;

    public JustForTestController(
        ILogger<JustForTestController> logger,
        IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _productRepository.GetAllAsync();
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> SeedProducts()
    {
        var product1 = new Product
        {
            Name = new Name ("product 1"),
            Category = new Category("category 1"),
            Price = new Price(14.99f),
        };

        var product2 = new Product
        {
            Name = new Name ("product 2"),
            Category = new Category ("category 1"),
            Price = new Price(24.99),
        };

        var product3 = new Product
        {
            Name = new Name ("product 3"),
            Category = new Category ("category 2"),
            Price = new Price(99.99f),
        };

        await _productRepository.InsertOneAsync(product1);
        await _productRepository.InsertOneAsync(product2);
        await _productRepository.InsertOneAsync(product3);

        return StatusCode(201);
    }
}
