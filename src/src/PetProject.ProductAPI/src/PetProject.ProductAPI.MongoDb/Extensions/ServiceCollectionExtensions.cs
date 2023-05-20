using Microsoft.Extensions.DependencyInjection;
using PetProject.ProductAPI.MongoDb.Contexts;
using PetProject.ProductAPI.MongoDb.Repositories;
using PetProject.ProductAPI.Domain.Interfaces.Repositories;

namespace PetProject.ProductAPI.MongoDb.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProductApiMongoDb(
        this IServiceCollection services,
        Action<DbContextOptions> optionsAction)
    {
        var _options = new DbContextOptions();
        optionsAction?.Invoke(_options);

        services.AddSingleton(_options);
        services.AddTransient<IDbContext, ProductApiDbContext>();
        services.AddTransient<IProductRepository, ProductRepository>();

        return services;
    }
}
