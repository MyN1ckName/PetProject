using PetProject.ProductAPI.Domain.Products;
using PetProject.ProductAPI.MongoDb.Contexts;
using PetProject.ProductAPI.MongoDb.Repositories;
using PetProject.ProductAPI.Domain.Manufacturers.Entitys;
using PetProject.ProductAPI.Domain.Exceptions;

namespace PetProject.ProductAPI.Domain.Tests.Integration;
public class ProductManagerTests
{
    private readonly ProductApiDbContext _dbContext;
    private readonly ProductManager _sut;

    public ProductManagerTests()
    {
        var dbContextOptions = new DbContextOptions()
        {
            ConnectionString = "mongodb://localhost:27017",
            DatabaseName = "ProductApiDatabase-TEST"
        };
        _dbContext = new ProductApiDbContext(dbContextOptions);
        var manufacturerRepository = new ManufacturerRepository(_dbContext);
        _sut = new ProductManager(manufacturerRepository);
    }


    [Fact]
    public async Task Create_product_with_exist_manufacturer_is_success()
    {
        await ClearDatabaseAsync();

        var name = "test_product";
        var category = "test_category";
        var price = 9.99f;
        var manufacturer = await AddTestManufacturerAsync();

        var product = await _sut.CreateProductAsync(name, category, price, manufacturer.Id);

        product.Should().NotBeNull();
        product.Name.Value.Should().Be(name);
        product.Category.Value.Should().Be(category);
        product.Price.Value.Should().Be(price);
        product.ManufacturerId.Should().Be(manufacturer.Id);
    }

    [Fact]
    public async Task Create_product_with_not_exist_manufacturer_is_fail()
    {
        await ClearDatabaseAsync();

        var name = "test_product";
        var category = "test_category";
        var price = 9.99f;
        var manufacturerId = Guid.NewGuid();

        var action = async () => await _sut.CreateProductAsync(name, category, price, manufacturerId);

        await action.Should().ThrowAsync<EntityNotFoundException>();
    }

    [Fact]
    public async Task Change_product_manufacturer_to_not_exist_is_fail()
    {
        await ClearDatabaseAsync();

        var name = "test_product";
        var category = "test_category";
        var price = 9.99f;
        var manufacturer = await AddTestManufacturerAsync();
        var manufacturerId = Guid.NewGuid();

        var product = await _sut.CreateProductAsync(name, category, price, manufacturer.Id);
        product.Should().NotBeNull();
        var action = async () => await _sut.ChangeManufacturerId(product, manufacturerId);

        await action.Should().ThrowAsync<EntityNotFoundException>();
    }

    private async Task ClearDatabaseAsync() =>
        await _dbContext.Manufacturer.Database.DropCollectionAsync(nameof(_dbContext.Manufacturer));

    private async Task<Manufacturer> AddTestManufacturerAsync()
    {
        var manufacturer = Manufacturer.Create("test_manafacturer", "https://example.com");
        await _dbContext.Manufacturer.InsertOneAsync(manufacturer);
        return manufacturer;
    }
}