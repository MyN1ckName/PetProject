using MongoDB.Driver;
using PetProject.ProductAPI.Domain;

namespace PetProject.ProductAPI.MongoDb.Contexts;

public interface IDbContext
{
    IMongoCollection<T> Collection<T>() where T : IEntity;
}
