using Microsoft.AspNetCore.Identity;

namespace PetProject.IdentityServer.Domain.Roles;
public class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole(string name) : base(name) { }
}
