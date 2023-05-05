namespace PetProject.ProductAPI.Domain.Product.ValueObject;

public class Price
{
    public const double MIN_PRICE = 9.99f;

    public Price(double price)
    {
        if (IsValidPrice(price))
            Value = price;
        else
            throw new ArgumentException(nameof(price));
    }

    public double Value { get; init; }

    private bool IsValidPrice(double price) => price >= MIN_PRICE;
}
