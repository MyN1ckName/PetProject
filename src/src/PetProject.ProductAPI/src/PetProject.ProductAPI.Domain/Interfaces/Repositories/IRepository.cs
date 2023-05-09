namespace PetProject.ProductAPI.Domain.Interfaces.Repositories;

public interface IRepository<TEntity, TKey>
{
    Task<TEntity> GetAsync(TKey key);
    Task<List<TEntity>> GetAllAsync();
    Task<TKey> InsertOneAsync(TEntity entity);
    Task UpdateOneAsync(TEntity entity);
    Task DeleteOneAsync(TKey key);
}
