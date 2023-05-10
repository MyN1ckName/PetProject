using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PetProject.IdentityServer.Domain.Users;
using PetProject.IdentityServer.Domain.Roles;
using PetProject.IdentityServer.Domain.OneDeviceLocks;

namespace PetProject.IdentityServer.Database.DbContexts;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<OneDeviceLock> DeviceLocks { get; set; }
}
