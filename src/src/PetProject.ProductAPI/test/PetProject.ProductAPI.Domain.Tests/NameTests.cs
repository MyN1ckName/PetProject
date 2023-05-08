namespace PetProject.ProductAPI.Domain.Tests;

public class NameTests
{
    [InlineData(null)]
    [InlineData("")]
    [Theory]
    public void Name_not_null_or_empty(string name)
    {
        Action action = () => new Name(name);
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Name_max_lenght_is_valid()
    {
        var name = new string('a', Name.MAX_NAME_LENGTH);
        var sut = new Name(name);
        sut.Value.Should().Be(name);
    }

    [Fact]
    public void Name_more_max_lenght_is_invalid()
    {
        var name = new string('a', Name.MAX_NAME_LENGTH + 1);
        var sut = new Name(name);
        sut.Value.Should().Be(name);
    }
}
