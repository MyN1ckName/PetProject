﻿namespace PetProject.ProductAPI.Domain.Tests;
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
        Assert.Throws<ArgumentException>(() => new Price(value));
    }
}