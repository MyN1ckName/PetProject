using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PetProject.IdentityServer.Database.DbContexts;

namespace PetProject.IdentityServer.Database.Factories;
internal class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        builder.UseNpgsql();
        return new ApplicationDbContext(builder.Options);
    }
}
