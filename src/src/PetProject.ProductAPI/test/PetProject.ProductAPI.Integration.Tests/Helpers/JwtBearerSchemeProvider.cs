using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace PetProject.ProductAPI.Integration.Tests.Helpers;

internal class JwtBearerSchemeProvider<THandler> : AuthenticationSchemeProvider
    where THandler : IAuthenticationHandler
{
    public JwtBearerSchemeProvider(IOptions<AuthenticationOptions> options)
        : base(options) { }

    protected JwtBearerSchemeProvider(
        IOptions<AuthenticationOptions> options,
        IDictionary<string, AuthenticationScheme> schemes)
        : base(options, schemes) { }

    public override Task<AuthenticationScheme> GetSchemeAsync(string name)
    {
        if (name == JwtBearerDefaults.AuthenticationScheme)
        {
            var scheme = new AuthenticationScheme(
                JwtBearerDefaults.AuthenticationScheme,
                JwtBearerDefaults.AuthenticationScheme,
                typeof(THandler)
            );
            return Task.FromResult(scheme);
        }
        return base.GetSchemeAsync(name);
    }
}
