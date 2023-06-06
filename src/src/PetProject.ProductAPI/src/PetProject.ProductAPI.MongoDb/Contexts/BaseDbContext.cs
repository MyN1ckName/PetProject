using MongoDB.Driver;
using PetProject.ProductAPI.Domain;

namespace PetProject.ProductAPI.MongoDb.Contexts;
public abstract class BaseDbContext : IDbContext
{
    private readonly IMongoDatabase _database;
    public BaseDbContext(
        DbContextOptions options)
    {
        if (options is null)
            throw new ArgumentNullException(nameof(options));

        var client = new MongoClient(options.ConnectionString);
        var databaseName = MongoUrl.Create(options.ConnectionString).DatabaseName;
        _database = client.GetDatabase(databaseName);
    }

    public virtual IMongoCollection<T> Collection<T>()
        where T : IEntity
    {
        return _database.GetCollection<T>(typeof(T).Name);
    }
}
