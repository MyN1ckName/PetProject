using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using System.Security.Claims;

namespace PetProject.IdentityServer.Host.ProfileServices;

internal class MachineIdProfileService : IProfileService
{
    public Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var claims = context.RequestedClaimTypes;

        if (context.ValidatedRequest.Raw.AllKeys.Contains("machine_name"))
        {

        }

        return Task.CompletedTask;
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        context.IsActive = true;
        return Task.CompletedTask;
    }
}
