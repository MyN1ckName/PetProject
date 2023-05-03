using Entity = PetProject.ProductAPI.Domain.Product.Entity;

namespace PetProject.ProductAPI.Domain.Interfaces.Repositories;

public interface IProductRepository : IRepository
{
    Task InsertOneAsync(Entity.Product product);
}
