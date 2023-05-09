using MongoDB.Driver;
using PetProject.ProductAPI.MongoDb.Contexts;
using PetProject.ProductAPI.Domain.Product.Entity;
using PetProject.ProductAPI.Domain.Interfaces.Repositories;
using PetProject.ProductAPI.Domain.Product.ValueObject;
using MongoDB.Bson;

namespace PetProject.ProductAPI.MongoDb.Repositories;
public class ProductRepository : IProductRepository<Guid>
{
    private readonly IMongoCollection<Product> _collection;

    public ProductRepository(IDbContext<Product> context)
    {
        _collection = context.Collection();
    }

    public async Task<Product> GetAsync(Guid id)
    {
        var filter = Builders<Product>
            .Filter
            .Eq(x => x.Id, id);

        var product = await _collection.FindAsync(filter);

        if (product is not null)
            return product.First();
        else
            throw new ArgumentException(nameof(id));
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _collection.AsQueryable().ToListAsync();
    }

    public async Task<Guid> InsertOneAsync(Product product)
    {
        await _collection.InsertOneAsync(product);
        return product.Id;
    }

    public async Task UpdateOneAsync(Product product)
    {
        var filter = Builders<Product>.Filter.Where(x => x.Id == product.Id);
        var update = Builders<Product>
            .Update
            .Set(x => x.Name, product.Name)
            .Set(x => x.Category, product.Category)
            .Set(x => x.Price, product.Price);

        await _collection.UpdateOneAsync(filter, update);
    }

    public async Task DeleteOneAsync(Guid id)
    {
        var filter = Builders<Product>.Filter.Where(x => x.Id == id);
        await _collection.DeleteOneAsync(filter);
    }
}
