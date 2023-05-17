using PetProject.ProductAPI.Host.Controllers;
using PetProject.ProductAPI.MongoDb.Contexts;
using PetProject.ProductAPI.MongoDb.Repositories;
using PetProject.ProductAPI.Application;
using PetProject.ProductAPI.Application.Services;
using PetProject.ProductAPI.Application.Contracts.Dto;
using PetProject.ProductAPI.Application.Contracts.Dto.Product;

namespace PetProject.ProductAPI.Host.Tests.Integration;

public class ProductControllerTests : IntegrationTest
{
    private readonly ProductController _controller;
    private readonly ProductApiDbContext _dbContext;

    public ProductControllerTests()
    {
        var dbContextOptions = new DbContextOptions()
        {
            ConnectionString = "mongodb://localhost:27017",
            DatabaseName = "ProductApiDatabase-TEST"
        };
        _dbContext = new ProductApiDbContext(dbContextOptions);
        var productRepository = new ProductRepository(_dbContext);

        var productAppService = new ProductAppService(
            CreateTestLogger<ProductAppService>(),
            productRepository,
            CreateTestMapper<AutomapperProfile>());

        _controller = new ProductController(
            CreateTestLogger<ProductController>(),
            productAppService);
    }

    [Fact]
    public async Task Insert_product_should_return_201_and_non_default_id()
    {
        await ClearDatabaseAsync();

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

    [Fact]
    public async Task Update_product_should_return_204_and_update_properties()
    {
        await ClearDatabaseAsync();

        var insertProduct = new CreateProductDto
        {
            Name = "product test name",
            Category = "test category",
            Price = 99.99f,
        };
        var token = new CancellationTokenSource().Token;
        var id = (((await _controller.InsertOneAsync(insertProduct, token)) as ObjectResult)!.Value as EntityDto<Guid>)!.Id;

        var productUpdate = new ProductDto
        {
            Id = id,
            Name = "new product test name",
            Category = "new test category",
            Price = 999.99f
        };

        var sut = await _controller.UpdateOneAsync(productUpdate, token);
        var result = sut.Should().BeOfType<StatusCodeResult>().Subject;
        result.StatusCode.Should().Be(204);

        var product = ((await _controller.GetAsync(id, token)) as ObjectResult)!.Value as ProductDto;

        product!.Name.Should().Be(productUpdate.Name);
        product!.Category.Should().Be(productUpdate.Category);
        Math.Round(product!.Price, 2).Should().Be(Math.Round(productUpdate.Price, 2));
    }

    private async Task ClearDatabaseAsync() =>
        await _dbContext.Product.Database.DropCollectionAsync(nameof(_dbContext.Product));
}
