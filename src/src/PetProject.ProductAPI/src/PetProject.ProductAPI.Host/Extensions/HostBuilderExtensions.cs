using Serilog;

namespace PetProject.ProductAPI.Host.Extensions;
internal static class HostBuilderExtensions
{
    public static IHostBuilder AddSerilog(this IHostBuilder host, IConfiguration configuration)
    {
        var loggerConfiguration = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration);

        var seqServerUrl = Environment.GetEnvironmentVariable("SEQ_SERVER_URL");

        if (seqServerUrl is not null)
            // loggerConfiguration.WriteTo.Seq(seqServerUrl);

        host.UseSerilog(loggerConfiguration.CreateLogger());

        return host;
    }
}
