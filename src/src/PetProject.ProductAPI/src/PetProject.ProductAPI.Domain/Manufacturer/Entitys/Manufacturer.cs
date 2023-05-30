using PetProject.ProductAPI.Domain.Manufacturer.ValueObjects;

namespace PetProject.ProductAPI.Domain.Manufacturer.Entitys;

public class Manufacturer : Entity<Guid>
{
    private Manufacturer() { }

    public Name Name { get; private set; }
    public City City { get; private set; }

    public static Manufacturer Create(string name, string city)
    {
        return new Manufacturer
        {
            Name = new Name(name),
            City = new City(city),
        };
    }

    public Manufacturer ChangeName(string name)
    {
        Name = new Name(name);
        return this;
    }

    public Manufacturer ChangeCity(string city)
    {
        City = new City(city);
        return this;
    }
}
