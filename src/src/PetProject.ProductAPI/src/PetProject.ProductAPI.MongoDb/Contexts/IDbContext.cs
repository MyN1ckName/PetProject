using MongoDB.Driver;

namespace PetProject.ProductAPI.MongoDb.Contexts;

public interface IDbContext<T>
{
    IMongoCollection<T> Collection();
}
