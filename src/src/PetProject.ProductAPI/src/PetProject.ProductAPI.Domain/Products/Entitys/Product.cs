using PetProject.ProductAPI.Domain.Products.ValueObjects;

namespace PetProject.ProductAPI.Domain.Products.Entitys;
public class Product : Entity<Guid>
{
    internal Product() { }

    public Name Name { get; internal set; }
    public Category Category { get; internal set; }
    public Price Price { get; internal set; }
    public Guid ManufacturerId { get; internal set; }

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
