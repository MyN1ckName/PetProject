using PetProject.ProductAPI.Domain.Product.ValueObjects;

namespace PetProject.ProductAPI.Domain.Product.Entity;
public class Product : Entity<Guid>
{
    public Product(string name, string category, double price)
    {
        Id = default;
        Name = new Name(name);
        Category = new Category(category);
        Price = new Price(price);
    }

    public Name Name { get; private set; }
    public Category Category { get; private set; }
    public Price Price { get; private set; }

    public Product SetName(string name)
    {
        Name = new Name(name);
        return this;
    }

    public Product SetCategory(string categoryName)
    {
        Category = new Category(categoryName);
        return this;
    }

    public Product SetPrice(double price)
    {
        Price = new Price(price);
        return this;
    }
}
