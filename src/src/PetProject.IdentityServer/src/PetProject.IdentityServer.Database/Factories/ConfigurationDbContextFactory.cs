using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Duende.IdentityServer.EntityFramework.Options;
using Duende.IdentityServer.EntityFramework.DbContexts;

namespace PetProject.IdentityServer.Database.Factories;
internal class ConfigurationDbContextFactory : IDesignTimeDbContextFactory<ConfigurationDbContext>
{
    public ConfigurationDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ConfigurationDbContext>();
        builder.UseNpgsql(b => b.MigrationsAssembly("PetProject.IdentityServer.Database"));
        return new ConfigurationDbContext(builder.Options)
        {
            StoreOptions = new ConfigurationStoreOptions(),
        };
    }
}
