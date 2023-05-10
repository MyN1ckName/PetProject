namespace PetProject.IdentityServer.Domain.OneDeviceLocks;
public class OneDeviceLock
{
    public Guid Id { get; set; }
    public string ClientId { get; set; }
    public Guid Hash { get; set; }
}
