using MongoDB.Driver;

namespace PetProject.ProductAPI.MongoDb.Contexts;
public abstract class BaseDbContext<T> : IDbContext<T>
{
    private readonly IMongoDatabase _database;
    public BaseDbContext(DbContextOptions options)
    {
        if (options is null)
            throw new ArgumentNullException(nameof(options));

        var client = new MongoClient(options.ConnectionString);
        _database = client.GetDatabase(options.DatabaseName);
    }

    public virtual IMongoCollection<T> Collection()
    {
        return _database.GetCollection<T>(nameof(T));
    }
}
