using MongoDB.Driver;

namespace PetProject.ProductAPI.MongoDb.Contexts;
public abstract class BaseDbContext<T> : IDbContext<T> where T : class
{
    private readonly IMongoDatabase _database;
    private readonly string _collectionName;
    public BaseDbContext(
        DbContextOptions options,
        string collectionName)
    {
        if (options is null)
            throw new ArgumentNullException(nameof(options));

        if (string.IsNullOrEmpty(collectionName))
            throw new ArgumentNullException(nameof(collectionName));

        var client = new MongoClient(options.ConnectionString);
        _database = client.GetDatabase(options.DatabaseName);
        _collectionName = collectionName;
    }

    public virtual IMongoCollection<T> Collection()
    {
        return _database.GetCollection<T>(_collectionName);
    }
}
