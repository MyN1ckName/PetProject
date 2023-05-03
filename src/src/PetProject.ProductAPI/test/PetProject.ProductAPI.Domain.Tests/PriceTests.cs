namespace PetProject.ProductAPI.Domain.Tests;
public class PriceTests
{
    [Fact]
    public void Price_more_minimum()
    {
        var value = 10f;
        var price = new Price(value);

        Assert.Equal(value, price.Value);
    }

    [Fact]
    public void Price_less_minimum()
    {
        var value = 9.98f;
        Assert.Throws<ArgumentException>(() => new Price(value));
    }
}
