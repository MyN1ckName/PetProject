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

    [HttpPost]
    public IActionResult SeedProducts()
    {
        var product = new Product
        {
            Name = new Name { Value = "product 1" },
            Category = new Category { Value = "category 1" },
            Price = new Price(10),
        };

        return Ok();
    }
}
