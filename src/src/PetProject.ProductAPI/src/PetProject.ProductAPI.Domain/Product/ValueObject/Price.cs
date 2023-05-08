namespace PetProject.ProductAPI.Domain.Product.ValueObject;

public class Price
{
    public const double MIN_PRICE = 9.99f;

    private double _value;

    public Price(double price)
    {
        Value = price;
    }

    public double Value
    {
        get => _value;
        private set
        {
            if (value >= MIN_PRICE)
                _value = value;
            else
                throw new ArgumentException("the price is below the minimum");
        }
    }
}
