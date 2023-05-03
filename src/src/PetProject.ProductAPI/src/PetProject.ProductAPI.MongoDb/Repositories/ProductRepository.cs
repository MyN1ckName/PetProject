using MongoDB.Driver;
using PetProject.ProductAPI.MongoDb.Contexts;
using PetProject.ProductAPI.Domain.Product.Entity;
using PetProject.ProductAPI.Domain.Interfaces.Repositories;

namespace PetProject.ProductAPI.MongoDb.Repositories;
public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _collection;

    public ProductRepository(IDbContext<Product> context)
    {
        _collection = context.Collection();
    }
}
