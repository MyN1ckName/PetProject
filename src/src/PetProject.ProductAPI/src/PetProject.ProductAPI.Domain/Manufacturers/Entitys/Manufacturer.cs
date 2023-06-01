using PetProject.ProductAPI.Domain.Manufacturers.ValueObjects;

namespace PetProject.ProductAPI.Domain.Manufacturers.Entitys;

public class Manufacturer : Entity<Guid>
{
    private Manufacturer() { }

    public Name Name { get; private set; }
    public string Website { get; private set; }

    public static Manufacturer Create(string name, string url)
    {
        var manufacturer = new Manufacturer
        {
            Name = new Name(name),
        };

        manufacturer.ChangeWebsite(url);
        return manufacturer;
    }

    public Manufacturer ChangeName(string name)
    {
        Name = new Name(name);
        return this;
    }

    public Manufacturer ChangeWebsite(string url)
    {
        Uri uriResult;

        bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        if (result)
            Website = url;
        else
            throw new ArgumentException(nameof(url));

        return this;
    }
}
