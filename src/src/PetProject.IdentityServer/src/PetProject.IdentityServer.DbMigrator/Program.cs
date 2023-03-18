using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using PetProject.IdentityServer.Database.Extensions;

namespace PetProject.IdentityServer.DbMigrator;
internal class Program
{
    static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();

        await CreateHostBuilder(configuration).RunConsoleAsync();
    }

    private static IHostBuilder CreateHostBuilder(IConfiguration configuration)
    {
        var logger = new LoggerConfiguration()
            .ReadFrom
            .Configuration(configuration)
            .CreateLogger();

        return Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddIdentityDatabase(options =>
                {
                    options.ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
                    options.IsDevelopment = true;
                });

                services.AddHostedService<MigratorHostedService>();
            }).UseSerilog(logger);
    }
}