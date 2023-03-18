using System.Diagnostics.CodeAnalysis;

namespace PetProject.IdentityServer.Database.Extensions;

public class IdentityDatabaseOptions
{
    [NotNull]
    public string ConnectionString { get; set; }
    public bool IsDevelopment { get; set; } = true;
}
