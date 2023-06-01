using PetProject.ProductAPI.MongoDb.Contexts;
using PetProject.ProductAPI.Domain.Manufacturers.Entitys;
using PetProject.ProductAPI.Domain.Interfaces.Repositories;

namespace PetProject.ProductAPI.MongoDb.Repositories;

public class ManufacturerRepository : RepositoryBase<Manufacturer, Guid>, IManufacturerRepository
{
    public ManufacturerRepository(IDbContext context) : base(context)
    {
    }
}
