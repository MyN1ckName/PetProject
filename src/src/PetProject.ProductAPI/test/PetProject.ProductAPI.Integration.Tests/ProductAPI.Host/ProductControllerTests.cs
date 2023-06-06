using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace PetProject.ProductAPI.Integration.Tests.ProductAPI.Host;

public class ProductControllerTests : IDisposable
{

    [Fact]
    public async Task First_integration_test()
    {
        OverrideEnvironmentVariable();

        var app = new WebApplicationFactory<Program>();

        var client = app.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IAuthenticationSchemeProvider, TestSchemeProvider>();
            });
        }).CreateClient();

        var url = "api/product";

        var response = await client.GetAsync(url);

        int i = 9;
    }

    private void OverrideEnvironmentVariable()
    {
        Environment.SetEnvironmentVariable("CONNECTION_STRING", "mongodb://localhost:27017");
        //Environment.SetEnvironmentVariable("DATABASE_NAME", "ProductApiDatabase-TEST");
        Environment.SetEnvironmentVariable("DATABASE_NAME", "ProductApiDatabase");
    }

    public void Dispose()
    {
        Environment.SetEnvironmentVariable("CONNECTION_STRING", "");
        Environment.SetEnvironmentVariable("DATABASE_NAME", "");
    }
}


public class TestSchemeProvider : AuthenticationSchemeProvider
{
    public TestSchemeProvider(IOptions<AuthenticationOptions> options)
        : base(options)
    {
    }

    protected TestSchemeProvider(
        IOptions<AuthenticationOptions> options,
        IDictionary<string, AuthenticationScheme> schemes
    )
        : base(options, schemes)
    {
    }

    public override Task<AuthenticationScheme> GetSchemeAsync(string name)
    {
        if (name == JwtBearerDefaults.AuthenticationScheme)
        {
            var scheme = new AuthenticationScheme(
                JwtBearerDefaults.AuthenticationScheme,
                JwtBearerDefaults.AuthenticationScheme,
                typeof(TestAuthHandler)
            );
            return Task.FromResult(scheme);
        }

        return base.GetSchemeAsync(name);
    }
}

public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public TestAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new[] {
            new Claim(ClaimTypes.Name, "Test user"),
            new Claim("scope", "product-api")
        };
        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, JwtBearerDefaults.AuthenticationScheme);

        var result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }
}
