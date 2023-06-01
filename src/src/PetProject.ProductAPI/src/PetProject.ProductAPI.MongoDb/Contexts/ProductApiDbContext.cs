using MongoDB.Driver;
using PetProject.ProductAPI.Domain.Products.Entitys;
using PetProject.ProductAPI.Domain.Manufacturer.Entitys;

namespace PetProject.ProductAPI.MongoDb.Contexts
{
    public class ProductApiDbContext : BaseDbContext
    {
        public ProductApiDbContext(DbContextOptions options) : base(options) { }
        public IMongoCollection<Product> Product => Collection<Product>();
        public IMongoCollection<Manufacturer> Manufacturer => Collection<Manufacturer>();
    }
}
