namespace PetProject.ProductAPI.Domain.Interfaces.Repositories;

public interface IRepository<TEntity, TKey> where TEntity : IEntity<TKey>
{
    Task<TEntity> GetAsync(TKey key, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TKey> InsertOneAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task ReplaceOneAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteOneAsync(TKey key, CancellationToken cancellationToken = default);
}
