using PetProject.ProductAPI.Domain.Manufacturer.ValueObjects;

namespace PetProject.ProductAPI.Domain.Manufacturer.Entity;

public class Manufacturer : Entity<Guid>
{
    public Name Name { get; set; }
    public City City { get; set; }
}
