namespace PetProject.ProductAPI.Domain.Product.ValueObjects;

public class Name
{
    public const int MAX_NAME_LENGTH = 50;
    private string _value;

    public Name(string name)
    {
        Value = name;
    }

    public string Value
    {
        get => _value;
        private set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("the name must not be empty");
            else if (value.Length > MAX_NAME_LENGTH)
                throw new ArgumentNullException($"the name is longer than {MAX_NAME_LENGTH} characters");
            else
                _value = value;
        }
    }
}
