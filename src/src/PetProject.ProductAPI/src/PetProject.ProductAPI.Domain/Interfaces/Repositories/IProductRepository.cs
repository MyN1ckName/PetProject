using PetProject.ProductAPI.Domain.Products.Entitys;

namespace PetProject.ProductAPI.Domain.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product, Guid>
{
}
