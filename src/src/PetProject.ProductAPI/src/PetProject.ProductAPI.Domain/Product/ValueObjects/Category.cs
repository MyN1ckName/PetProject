namespace PetProject.ProductAPI.Domain.Product.ValueObjects;

public class Category
{
    public const int MAX_CATEGORY_LENGHT = 50;
    private string _value;

    public Category(string name)
    {
        Value = name;
    }

    public string Value
    {
        get => _value;
        private set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("the category name must not be empty");
            else if (value.Length > MAX_CATEGORY_LENGHT)
                throw new ArgumentNullException($"the category name is longer than {MAX_CATEGORY_LENGHT} characters");
            else
                _value = value;
        }
    }
}
