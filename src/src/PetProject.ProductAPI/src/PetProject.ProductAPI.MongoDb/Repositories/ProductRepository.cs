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

    // public override async Task UpdateOneAsync(Product product, CancellationToken cancellationToken = default)
    // {
    //     var filter = Builders<Product>.Filter.Where(x => x.Id == product.Id);
    //     var update = Builders<Product>
    //         .Update
    //         .Set(x => x.Name, product.Name)
    //         .Set(x => x.Category, product.Category)
    //         .Set(x => x.Price, product.Price);
    // 
    //     await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    // }
}
