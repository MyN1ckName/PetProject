using PetProject.ProductAPI.Host.Controllers;
using PetProject.ProductAPI.MongoDb.Contexts;
using PetProject.ProductAPI.MongoDb.Repositories;
using PetProject.ProductAPI.Application;
using PetProject.ProductAPI.Application.Services;
using PetProject.ProductAPI.Application.Contracts.Dto;
using PetProject.ProductAPI.Application.Contracts.Dto.Product;

namespace PetProject.ProductAPI.Host.Tests.Integration;

public class ProductControllerTests
{
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        var dbContextOptions = new DbContextOptions()
        {
            ConnectionString = "mongodb://localhost:27017",
            DatabaseName = "ProductApiDatabase-TEST"
        };
        var dbContext = new ProductApiDbContext(dbContextOptions);
        var productRepository = new ProductRepository(dbContext);

        var productAppService = new ProductAppService(
            CreateTestLogger<ProductAppService>(),
            productRepository,
            CreateTestMapper());

        _controller = new ProductController(
            CreateTestLogger<ProductController>(),
            productAppService);
    }

    [Fact]
    public async void Insert_product()
    {
        var product = new CreateProductDto
        {
            Name = "product test name",
            Category = "test category",
            Price = 99.99f,
        };
        var token = new CancellationTokenSource().Token;

        var sut = await _controller.InsertOneAsync(product, token);

        var result = sut.Should().BeOfType<ObjectResult>().Subject;
        result.StatusCode.Should().Be(201);
        var value = result.Value.Should().BeOfType<EntityDto<Guid>>().Subject;
        value.Id.Should().NotBe(default(Guid));
    }

    private ILogger<T> CreateTestLogger<T>() => new LoggerFactory().CreateLogger<T>();
    private IMapper CreateTestMapper() =>
        new MapperConfiguration(cfg => cfg.AddProfile<AutomapperProfile>()).CreateMapper();
}
