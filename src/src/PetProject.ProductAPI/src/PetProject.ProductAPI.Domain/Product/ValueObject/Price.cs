namespace PetProject.ProductAPI.Domain.Product.ValueObject;

public class Price
{
    public const double MIN_PRICE = 9.99f;
    public Price(double price)
    {
        if (Validate(price))
            Value = price;
    }
    public double Value { get; init; }

    private bool Validate(double price)
    {
        if (price >= MIN_PRICE)
            return true;
        else
            throw new ArgumentException(nameof(price));
    }
}
