using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetProject.IdentityServer.Database.DbContexts;
using PetProject.IdentityServer.Domain.Users;
using PetProject.IdentityServer.Domain.Roles;

namespace PetProject.IdentityServer.Database.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityDatabase(
        this IServiceCollection services,
        Action<IdentityDatabaseOptions> optionsAction)
    {
        var migrationsAssembly = "PetProject.IdentityServer.Database";

        var _options = new IdentityDatabaseOptions();
        optionsAction?.Invoke(_options);

        services
            .AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(_options.ConnectionString);
            });

        services
            .AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        var identity = services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                options.EmitStaticAudienceClaim = true;
            })
            .AddAspNetIdentity<ApplicationUser>()
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseNpgsql(_options.ConnectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseNpgsql(_options.ConnectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            });

        if (_options.IsDevelopment)
        {
            // identity.AddDeveloperSigningCredential();
        }

        return services;
    }
}

