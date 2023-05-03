namespace PetProject.ProductAPI.Domain.Product.ValueObject;

public class Price
{
    public const float MIN_PRICE = 9.99f;
    public Price(float price)
    {
        if (Validate(price))
            Value = price;
    }
    public float Value { get; }

    private bool Validate(float price)
    {
        if (price >= MIN_PRICE)
            return true;
        else
            throw new ArgumentException(nameof(price));
    }
}
