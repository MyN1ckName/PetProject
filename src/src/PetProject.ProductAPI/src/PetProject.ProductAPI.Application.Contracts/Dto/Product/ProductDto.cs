namespace PetProject.ProductAPI.Application.Contracts.Dto.Product;

public class ProductDto : EntityDto<Guid>
{
    public string Name { get; init; }
    public string Category { get; init; }
    public double Price { get; init; }
}
