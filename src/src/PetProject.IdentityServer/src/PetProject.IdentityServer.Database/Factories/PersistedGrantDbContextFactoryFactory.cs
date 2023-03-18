using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Duende.IdentityServer.EntityFramework.Options;
using Duende.IdentityServer.EntityFramework.DbContexts;

namespace PetProject.IdentityServer.Database.Factories;
internal class PersistedGrantDbContextFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext>
{
    public PersistedGrantDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<PersistedGrantDbContext>();
        builder.UseNpgsql(b => b.MigrationsAssembly("PetProject.IdentityServer.Database"));
        return new PersistedGrantDbContext(builder.Options)
        {
            StoreOptions = new OperationalStoreOptions(),
        };
    }
}
