using MongoDB.Driver;
using PetProject.ProductAPI.MongoDb.Contexts;
using PetProject.ProductAPI.Domain.Product.Entitys;
using PetProject.ProductAPI.Domain.Interfaces.Repositories;

namespace PetProject.ProductAPI.MongoDb.Repositories;
public class ProductRepository : RepositoryBase<Product, Guid>,  IProductRepository
{
    private readonly IMongoCollection<Product> _collection;

    public ProductRepository(IDbContext context)
        : base(context)
    {
        _collection = context.Collection<Product>();
    }
}
