namespace PetProject.ProductAPI.Domain.Manufacturer.ValueObjects;

public class City : ValueObject
{
    public string Value { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value; 
    }
}
