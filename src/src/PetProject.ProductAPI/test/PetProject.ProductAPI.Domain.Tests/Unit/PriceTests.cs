using PetProject.ProductAPI.Domain.Products.ValueObjects;

namespace PetProject.ProductAPI.Domain.Tests.Unit;
public class PriceTests
{
    [InlineData(9.99f)]
    [InlineData(10f)]
    [InlineData(999.999f)]
    [Theory]
    public void This_price_is_valid(double value)
    {
        var sut = new Price(value); // SUT - system under test
        sut.Value.Should().Be(value);
    }

    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(9.98f)]
    [Theory]
    public void This_price_is_invalid(double value)
    {
        Action action = () => new Price(value);
        action.Should().Throw<ArgumentException>();
    }
}
