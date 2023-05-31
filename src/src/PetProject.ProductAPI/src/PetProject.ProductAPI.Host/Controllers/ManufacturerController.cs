using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PetProject.ProductAPI.Domain.Manufacturer.Entitys;
using PetProject.ProductAPI.Domain.Interfaces.Repositories;
using PetProject.ProductAPI.Application.Contracts.Dto.Manufacturer;

namespace PetProject.ProductAPI.Host.Controllers;

[ApiController]
[Authorize(Policy = "manufacturer-api")]
[Route("api/[controller]")]
public class ManufacturerController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IManufacturerRepository _repository;

    public ManufacturerController(
        ILogger<ManufacturerController> logger,
        IManufacturerRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateManufacturerDto input)
    {
        var manufaturer = Manufacturer.Create(input.Name, input.City);
        await _repository.InsertOneAsync(manufaturer);

        // return Created();
        return new StatusCodeResult(201);
    }
}
