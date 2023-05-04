namespace PetProject.ProductAPI.Domain.Interfaces.Repositories;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task InsertOneAsync(T entity);
}
