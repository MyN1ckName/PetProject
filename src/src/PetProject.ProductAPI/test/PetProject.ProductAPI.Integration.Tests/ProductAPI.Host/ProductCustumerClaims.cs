using System.Security.Claims;
using PetProject.ProductAPI.Integration.Tests.Helpers;

namespace PetProject.ProductAPI.Integration.Tests.ProductAPI.Host;
internal class ProductCustumerClaims : IAuthenticateResultClaims
{
    public static IEnumerable<Claim> Claims { get; set; } = new[]
    {
        new Claim(ClaimTypes.Name, "Test user"),
        new Claim(ClaimTypes.Role, "customer"),
        new Claim("scope", "product-api")
    };
}
