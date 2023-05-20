namespace PetProject.ProductAPI.Domain.Manufacturer.ValueObjects;

public class Name : ValueObject
{
    public string Value { get; init; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
