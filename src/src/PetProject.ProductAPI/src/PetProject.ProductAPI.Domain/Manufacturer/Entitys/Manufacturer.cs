using PetProject.ProductAPI.Domain.Manufacturer.ValueObjects;

namespace PetProject.ProductAPI.Domain.Manufacturer.Entitys;

public class Manufacturer : Entity<Guid>
{
    private Manufacturer() { }

    public Name Name { get; private set; }
    public string Website { get; private set; }

    public static Manufacturer Create(string name, string url)
    {
        return new Manufacturer
        {
            Name = new Name(name),
            Website = url,
        };
    }

    public Manufacturer ChangeName(string name)
    {
        Name = new Name(name);
        return this;
    }

    public Manufacturer ChangeWebsite(string url)
    {
        Website = url;
        return this;
    }
}
