using System.ComponentModel.DataAnnotations;

namespace PetProject.ProductAPI.Application.Contracts.Dto.Manufacturer;

public class CreateManufacturerDto
{
    public string Name { get; set; }
    [Url]
    public string Website { get; set; }
}
