using PetProject.IdentityServer.Host.Middlewares;

namespace PetProject.IdentityServer.Host.Extensions;
public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseOneActiveDeviceLock(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<OneActiveDeviceMiddleware>();
    }
}
