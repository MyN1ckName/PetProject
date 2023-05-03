using MongoDB.Driver;
using PetProject.ProductAPI.Domain.Product.Entity;

namespace PetProject.ProductAPI.MongoDb.Contexts
{
    public class ProductApiDbContext : BaseDbContext<Product>
    {
        public ProductApiDbContext(DbContextOptions options)
            : base(options) { }
        public IMongoCollection<Product> Product => Collection();
    }
}
