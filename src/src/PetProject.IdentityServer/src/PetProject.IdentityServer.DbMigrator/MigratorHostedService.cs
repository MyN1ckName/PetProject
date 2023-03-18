using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Duende.IdentityServer.EntityFramework.DbContexts;
using PetProject.IdentityServer.Database.DbContexts;

namespace PetProject.IdentityServer.DbMigrator;

internal class MigratorHostedService : IHostedService
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly ILogger<MigratorHostedService> _logger;
    private readonly List<DbContext> _contexts;

    public MigratorHostedService(
        IHostApplicationLifetime hostApplicationLifetime,
        ILogger<MigratorHostedService> logger,
        ApplicationDbContext applicationDbContext,
        PersistedGrantDbContext persistedGrantDbContext,
        ConfigurationDbContext configurationDbContext)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
        _logger = logger;
        _contexts = new List<DbContext>
        {
            applicationDbContext,
            persistedGrantDbContext,
            configurationDbContext
        };
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _contexts.ForEach(x =>
        {
            Migrate(x);
        });

        _hostApplicationLifetime.StopApplication();
    }

    private void Migrate<T>(T context)
        where T : DbContext
    {
        var pendingMigrations = context.Database.GetPendingMigrations();

        if (pendingMigrations.Any())
        {
            _logger.LogInformation("Migrate {DdContextName}", context.ToString());
            context.Database.Migrate();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
