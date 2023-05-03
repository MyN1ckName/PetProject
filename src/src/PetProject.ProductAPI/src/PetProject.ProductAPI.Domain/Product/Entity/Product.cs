using PetProject.ProductAPI.Domain.Product.ValueObject;

namespace PetProject.ProductAPI.Domain.Product.Entity;
public class Product
{
    public Guid Id { get; init; }
    public Name Name{ get; init; }
    public Category Category { get; init; }
    public Price Price { get; init; }
}
