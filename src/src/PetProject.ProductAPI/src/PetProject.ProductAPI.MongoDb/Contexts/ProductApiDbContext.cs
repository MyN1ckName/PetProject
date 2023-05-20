using MongoDB.Driver;
using PetProject.ProductAPI.Domain.Product.Entity;
using PetProject.ProductAPI.Domain.Manufacturer.Entity;

namespace PetProject.ProductAPI.MongoDb.Contexts
{
    public class ProductApiDbContext : BaseDbContext
    {
        public ProductApiDbContext(DbContextOptions options) : base(options) { }
        public IMongoCollection<Product> Product => Collection<Product>();
        public IMongoCollection<Manufacturer> Manufacturer => Collection<Manufacturer>();
    }
}
