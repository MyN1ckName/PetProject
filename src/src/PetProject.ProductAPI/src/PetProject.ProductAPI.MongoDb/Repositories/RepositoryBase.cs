using MongoDB.Driver;
using PetProject.ProductAPI.Domain;
using PetProject.ProductAPI.MongoDb.Contexts;
using PetProject.ProductAPI.Domain.Exceptions;
using PetProject.ProductAPI.Domain.Interfaces.Repositories;

namespace PetProject.ProductAPI.MongoDb.Repositories;
public class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : IEntity<TKey>
{
    private readonly IMongoCollection<TEntity> _collection;

    public RepositoryBase(IDbContext context)
    {
        _collection = context.Collection<TEntity>();
    }

    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _collection.AsQueryable().ToListAsync(cancellationToken);
    }

    public async Task<TEntity> GetAsync(TKey key, CancellationToken cancellationToken = default)
    {
        var filter = Builders<TEntity>
            .Filter
            .Eq(x => x.Id, key);

        try
        {
            return await _collection.Find(filter).FirstAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new EntityNotFoundException($"the {typeof(TEntity).Name} not found", ex);
        }
    }

    public async Task<TKey> InsertOneAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        return entity.Id;
    }

    public virtual Task UpdateOneAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteOneAsync(TKey key, CancellationToken cancellationToken = default)
    {
        var filter = Builders<TEntity>.Filter.Eq(x => x.Id, key);
        await _collection.DeleteOneAsync(filter, cancellationToken: cancellationToken);
    }
}
