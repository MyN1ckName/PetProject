using System.Security.Claims;

namespace PetProject.ProductAPI.Integration.Tests.Helpers;
internal interface IAuthenticateResultClaims
{
    public virtual static IEnumerable<Claim> Claims { get; set; }
}
