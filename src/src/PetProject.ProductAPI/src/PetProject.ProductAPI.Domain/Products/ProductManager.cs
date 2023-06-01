using PetProject.ProductAPI.Domain.Products.Entitys;
using PetProject.ProductAPI.Domain.Products.ValueObjects;
using PetProject.ProductAPI.Domain.Interfaces.Repositories;

namespace PetProject.ProductAPI.Domain.Products;

public class ProductManager
{
    private readonly IManufacturerRepository _manufacturerRepository;
    public ProductManager(IManufacturerRepository manufacturerRepository)
    {
        _manufacturerRepository = manufacturerRepository ?? throw new ArgumentNullException(nameof(manufacturerRepository));
    }

    public async Task<Product> CreateProductAsync(string name, string category, double price, Guid manufacturerId)
    {
        var manufacturer = await _manufacturerRepository.GetAsync(manufacturerId);

        var product = new Product
        {
            Name = new Name(name),
            Category = new Category(category),
            Price = new Price(price),
            ManufacturerId = manufacturer.Id,
        };
        return product;            
    }
}
