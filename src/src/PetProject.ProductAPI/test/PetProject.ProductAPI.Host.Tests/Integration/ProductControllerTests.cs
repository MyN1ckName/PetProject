﻿using PetProject.ProductAPI.Host.Controllers;
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
        // Arrange
        await ClearDatabaseAsync();

        var product = new CreateProductDto
        {
            Name = "product test name",
            Category = "test category",
            Price = 99.99f,
        };
        var token = new CancellationTokenSource().Token;

        // Act
        var sut = await _controller.InsertOneAsync(product, token);

        // Assert
        var result = sut.Should().BeOfType<ObjectResult>().Subject;
        result.StatusCode.Should().Be(201);
        var value = result.Value.Should().BeOfType<EntityDto<Guid>>().Subject;
        value.Id.Should().NotBe(default(Guid));
    }

    [Fact]
    public async Task Replace_product_should_return_204_and_update_all_properties()
    {
        // Arrange
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

        productUpdate.Name.Should().NotBe(insertProduct.Name);
        productUpdate.Category.Should().NotBe(insertProduct.Category);
        Math.Round(productUpdate.Price, 2).Should().NotBe(Math.Round(insertProduct.Price, 2));

        //Act
        var sut = await _controller.ReplaceOneAsync(productUpdate, token);

        // Assert
        var result = sut.Should().BeOfType<StatusCodeResult>().Subject;
        result.StatusCode.Should().Be(204);

        var product = ((await _controller.GetAsync(id, token)) as ObjectResult)!.Value as ProductDto;

        product!.Name.Should().Be(productUpdate.Name);
        product!.Category.Should().Be(productUpdate.Category);
        Math.Round(product!.Price, 2).Should().Be(Math.Round(productUpdate.Price, 2));
    }

    [Fact]
    public async Task Delete_product_should_return_204_and_delete()
    {
        // Arrange
        await ClearDatabaseAsync();

        var insertProduct = new CreateProductDto
        {
            Name = "product test name",
            Category = "test category",
            Price = 99.99f,
        };
        var token = new CancellationTokenSource().Token;
        var id = (((await _controller.InsertOneAsync(insertProduct, token)) as ObjectResult)!.Value as EntityDto<Guid>)!.Id;

        //Act
        var sut = await _controller.DeleteOneAsync(id);

        //Assert
        var result = sut.Should().BeOfType<StatusCodeResult>().Subject;
        result.StatusCode.Should().Be(204);

        Func<Task<IActionResult>> act = async () => { return await _controller.GetAsync(id, token); };
        await act.Should().ThrowAsync<Exception>();
    }

    private async Task ClearDatabaseAsync() =>
        await _dbContext.Product.Database.DropCollectionAsync(nameof(_dbContext.Product));
}
