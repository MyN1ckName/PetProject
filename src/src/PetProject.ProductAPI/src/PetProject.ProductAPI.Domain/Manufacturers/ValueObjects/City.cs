namespace PetProject.ProductAPI.Domain.Manufacturers.ValueObjects;

public class City : ValueObject
{
    public City(string city)
    {
        Value = city;
    }

    public string Value { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
