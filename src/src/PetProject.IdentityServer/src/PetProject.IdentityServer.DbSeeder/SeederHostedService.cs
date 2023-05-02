using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using IdentityModel;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.EntityFramework.DbContexts;
using PetProject.IdentityServer.Domain.Roles;
using PetProject.IdentityServer.Domain.Users;
using PetProject.IdentityServer.Database.DbContexts;

namespace PetProject.IdentityServer.DbSeeder;

internal class SeederHostedService : IHostedService
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly ILogger<SeederHostedService> _logger;
    private readonly ConfigurationDbContext _configurationContext;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _applicationContext;

    public SeederHostedService(
        IHostApplicationLifetime hostApplicationLifetime,
        ILogger<SeederHostedService> logger,
        ConfigurationDbContext configurationContext,
        RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
        _logger = logger;
        _configurationContext = configurationContext;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await SeedIdentityResourcesAsync();
        await SeedApiScopesAsync();
        await SeedApiClientsAsync();
        await SeedRolesAsync();
        await SeedUsersAsync();
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

    private async Task SeedRolesAsync()
    {
        _logger.LogInformation("Seed {EntityName} Start", "ApplicationRole");
        foreach (var role in SeedData.Roles)
        {
            var exist = await _roleManager.FindByNameAsync(role.Name);
            if (exist is null)
            {
                await _roleManager.CreateAsync(role);
                _logger.LogInformation("Role {RoleName} add", role.Name);
            }
            else
            {
                _logger.LogInformation("Role {RoleName} found and skip seed, Id = {Id}", role.Name, exist.Id);
            }
        }
        _logger.LogInformation("Seed {EntityName} End", "ApplicationRole");
    }

    private async Task SeedUsersAsync()
    {
        _logger.LogInformation("Seed {EntityName} Start", "ApplicationUser");

        var admin = new ApplicationUser()
        {
            UserName = "admin@example.com",
            Email = "admin@example.com",
            EmailConfirmed = true,
            PhoneNumber = "111111111111",
        };

        await _userManager.CreateAsync(admin, "Password!1");
        await _userManager.AddToRoleAsync(admin, SeedData.ADMIN_ROLE);
        await _userManager.AddClaimsAsync(admin, new Claim[] { new Claim(JwtClaimTypes.Role, SeedData.ADMIN_ROLE) });

        var customer1 = new ApplicationUser()
        {
            UserName = "customer1@example.com",
            Email = "customer1@example.com",
            EmailConfirmed = true,
            PhoneNumber = "222222222222",
        };

        await _userManager.CreateAsync(customer1, "Password!1");
        await _userManager.AddToRoleAsync(customer1, SeedData.CUSTOMER_ROLE);
        await _userManager.AddClaimsAsync(admin, new Claim[] { new Claim(JwtClaimTypes.Role, SeedData.CUSTOMER_ROLE) });

        _logger.LogInformation("Seed {EntityName} End", "ApplicationUser");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}