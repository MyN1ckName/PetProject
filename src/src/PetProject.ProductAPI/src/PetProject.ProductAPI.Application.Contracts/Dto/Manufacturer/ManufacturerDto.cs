namespace PetProject.ProductAPI.Application.Contracts.Dto.Manufacturer;

public class ManufacturerDto : EntityDto<Guid>
{
    public string Name { get; set; }
    public string Website { get; set; }
}
