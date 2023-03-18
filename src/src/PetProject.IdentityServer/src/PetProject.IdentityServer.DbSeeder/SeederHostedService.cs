using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.EntityFramework.DbContexts;

namespace PetProject.IdentityServer.DbSeeder;

internal class SeederHostedService : IHostedService
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly ILogger<SeederHostedService> _logger;
    private readonly ConfigurationDbContext _configurationContext;

    public SeederHostedService(
        IHostApplicationLifetime hostApplicationLifetime,
        ILogger<SeederHostedService> logger,
        ConfigurationDbContext configurationContext)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
        _logger = logger;
        _configurationContext = configurationContext;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await SeedIdentityResourcesAsync();
        await SeedApiScopesAsync();
        await SeedApiClientsAsync();
        _hostApplicationLifetime.StopApplication();
    }

    private async Task SeedIdentityResourcesAsync()
    {
        _logger.LogInformation("Seed {EntityName} Start", "IdentityResources");

        if (_configurationContext.Database.GetPendingMigrations().Any())
        {
            _logger.LogInformation("Migrate {ContextName}", "ConfigurationDbContext");
            _configurationContext.Database.Migrate();
        }

        foreach (var resource in SeedData.IdentityResources)
        {
            var result = _configurationContext.IdentityResources
                  .Where(x => x.Name == resource.Name)
                  .FirstOrDefault();

            if (result == null)
            {
                _configurationContext.IdentityResources.Add(resource.ToEntity());
                _logger.LogInformation("IdentityResource {IdentityResourceName} add", resource.Name);
            }
            else
            {
                _logger.LogInformation("IdentityResources {ResourceName} found and skip seed, Id = {Id}", resource.Name, result.Id);
            }
        }

        await _configurationContext.SaveChangesAsync();
        _logger.LogInformation("Seed {EntityName} End", "IdentityResources");
    }

    private async Task SeedApiScopesAsync()
    {
        _logger.LogInformation("Seed {EntityName} Start", "SeedApi");

        if (_configurationContext.Database.GetPendingMigrations().Any())
        {
            _logger.LogInformation("Migrate {ContextName}", "ConfigurationDbContext");
            _configurationContext.Database.Migrate();
        }

        foreach (var scope in SeedData.ApiScopes)
        {
            var result = _configurationContext.ApiScopes
                  .Where(x => x.Name == scope.Name)
                  .FirstOrDefault();

            if (result == null)
            {
                _configurationContext.ApiScopes.Add(scope.ToEntity());
                _logger.LogInformation("ApiScope {ScopeName} add", scope.Name);
            }
            else
            {
                _logger.LogInformation("ApiScope {ScopeName} found and skip seed, Id = {Id}", scope.Name, result.Id);
            }
        }

        await _configurationContext.SaveChangesAsync();
        _logger.LogInformation("Seed {EntityName} End", "SeedApi");
    }

    private async Task SeedApiClientsAsync()
    {
        _logger.LogInformation("Seed {EntityName} Start", "Client");

        if (_configurationContext.Database.GetPendingMigrations().Any())
        {
            _logger.LogInformation("Migrate {ContextName}", "ConfigurationDbContext");
            _configurationContext.Database.Migrate();
        }

        foreach (var client in SeedData.Clients)
        {
            var result = _configurationContext.Clients
                  .Where(x => x.ClientId == client.ClientId)
                  .FirstOrDefault();

            if (result == null)
            {
                _configurationContext.Clients.Add(client.ToEntity());
                _logger.LogInformation("Client {ScopeName} add", client.ClientId);
            }
            else
            {
                _logger.LogInformation("Client {ClientId} found and skip seed, Id = {Id}", client.ClientId, result.Id);
            }
        }

        await _configurationContext.SaveChangesAsync();
        _logger.LogInformation("Seed {EntityName} End", "Client");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}