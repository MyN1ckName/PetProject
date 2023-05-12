namespace PetProject.ProductAPI.Domain.Interfaces.Repositories;

public interface IRepository<TEntity, TKey>
{
    Task<TEntity> GetAsync(TKey key, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToke = default);
    Task<TKey> InsertOneAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateOneAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteOneAsync(TKey key, CancellationToken cancellationToken = default);
}
