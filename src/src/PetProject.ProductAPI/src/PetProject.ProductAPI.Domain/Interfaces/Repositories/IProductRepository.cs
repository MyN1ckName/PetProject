using Entity = PetProject.ProductAPI.Domain.Product.Entity;

namespace PetProject.ProductAPI.Domain.Interfaces.Repositories;

public interface IProductRepository : IRepository<Entity.Product>
{
}
