using PetProject.ProductAPI.Domain.Manufacturer.ValueObjects;

namespace PetProject.ProductAPI.Domain.Manufacturer.Entitys;

public class Manufacturer : Entity<Guid>
{
    private Manufacturer() { }

    public static Manufacturer Create(string name, string city)
    {
        return new Manufacturer
        {
            Name = new Name(name),
            City = new City(city),
        };
    }

    public Name Name { get; init; }
    public City City { get; init; }
}
