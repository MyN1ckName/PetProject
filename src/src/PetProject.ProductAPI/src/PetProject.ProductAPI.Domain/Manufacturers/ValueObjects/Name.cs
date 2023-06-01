namespace PetProject.ProductAPI.Domain.Manufacturers.ValueObjects;

public class Name : ValueObject
{
    public Name(string name)
    {
        Value = name;
    }

    public string Value { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
