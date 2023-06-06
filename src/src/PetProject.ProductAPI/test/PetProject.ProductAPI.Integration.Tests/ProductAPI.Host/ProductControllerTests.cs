using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using PetProject.ProductAPI.MongoDb.Contexts;
using PetProject.ProductAPI.Integration.Tests.Helpers;

namespace PetProject.ProductAPI.Integration.Tests.ProductAPI.Host;

public class ProductControllerTests
{
    [Fact]
    public async Task First_integration_test()
    {
        var client = GetHttpClient<ProductCustumerClaims>();
        var url = "api/product";
        var response = await client.GetAsync(url);

        Assert.True(response.IsSuccessStatusCode);
    }

    private HttpClient GetHttpClient<TAuthenticateResultClaims>()
        where TAuthenticateResultClaims : IAuthenticateResultClaims
    {
        var dbContextOptions = new DbContextOptions
        {
            ConnectionString = "mongodb://localhost:27017/ProductApiDatabase-TEST",
        };

        var app = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IAuthenticationSchemeProvider, JwtBearerSchemeProvider<JwtBearerAuthHandler<TAuthenticateResultClaims>>>();
                    services.AddSingleton(dbContextOptions);
                });
            });

        var client = app.CreateClient();

        return client;
    }
}
